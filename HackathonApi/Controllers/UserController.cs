using ABCBrasil.Providers.BasicContractProvider.Lib;
using Domain.Interfaces.Services;
using Domain.Models.Dto.Requests;
using Domain.Models.Dto.Responses;
using Domain.Models.Entities;
using HackathonApi.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HackathonApi.Controllers
{
    [Route("api/v1/[controller]")]
    [AbcAuthorization()]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly INotificationManager _notificationManager;

        public UserController(INotificationManager notificationManager, IUserService userService)
        {
            _notificationManager = notificationManager;
            _userService = userService;
        }

        /// <summary>
        /// Endpoint para criação de usuário.
        /// </summary>
        /// <returns>Confirmação e dados do usuário criado</returns>
        /// <example></example>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseBase<User>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseBase<User>))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ResponseBase<User>))]
        public async Task<IActionResult> Post([FromBody] UserRequest request)
        {
            try
            {
                var result = await _userService.Create(request);

                if (result.IsSuccess)
                {
                    return Ok(new ResponseBase<User>(result.GetSuccess()));
                }

                if (result.GetFailure().Any(x => x.Type.Equals("Critical")))
                {
                    return new ObjectResult(result.GetFailure()) { StatusCode = (int?)HttpStatusCode.InternalServerError };
                }

                return UnprocessableEntity(result.GetFailure());
            }
            catch (Exception e)
            {
                return new ObjectResult(_notificationManager.AddCritical(e.Message).GetNotifications()) { StatusCode = (int?)HttpStatusCode.InternalServerError };
            }
        }

        /// <summary>
        /// Endpoint para exclusão de usuário.
        /// </summary>
        /// <returns>Confirmação de exclusão</returns>
        /// <example></example>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseBase<User>))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ResponseBase<User>))]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            try
            {
                var result = await _userService.Delete(id);

                if (result.IsSuccess)
                {
                    return Ok();
                }

                if (result.GetFailure().Any(x => x.Type.Equals("Critical")))
                {
                    return new ObjectResult(result.GetFailure()) { StatusCode = (int?)HttpStatusCode.InternalServerError };
                }

                return UnprocessableEntity(result.GetFailure());
            }
            catch (Exception e)
            {
                return new ObjectResult(_notificationManager.AddCritical(e.Message).GetNotifications()) { StatusCode = (int?)HttpStatusCode.InternalServerError };
            }
        }

        /// <summary>
        /// Endpoint para consulta de usuário.
        /// </summary>
        /// <returns>Retorna usuário</returns>
        /// <example></example>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseBase<User>))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ResponseBase<User>))]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            try
            {
                var result = await _userService.Get(id);

                if (result.IsSuccess)
                {
                    return result.GetSuccess() != null ? Ok(new ResponseBase<User>(result.GetSuccess())) : NotFound();
                }

                if (result.GetFailure().Any(x => x.Type.Equals("Critical")))
                {
                    return new ObjectResult(result.GetFailure()) { StatusCode = (int?)HttpStatusCode.InternalServerError };
                }

                return UnprocessableEntity(result.GetFailure());
            }
            catch (Exception e)
            {
                return new ObjectResult(_notificationManager.AddCritical(e.Message).GetNotifications()) { StatusCode = (int?)HttpStatusCode.InternalServerError };
            }
        }

        /// <summary>
        /// Endpoint para criação de usuário.
        /// </summary>
        /// <returns>Confirmação e dados do usuário criado</returns>
        /// <example></example>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseBase<User>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseBase<User>))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ResponseBase<User>))]
        public async Task<IActionResult> Put([FromBody] UserRequest request, [FromRoute] string id)
        {
            try
            {
                var result = await _userService.Update(request, id);

                if (result.IsSuccess)
                {
                    return Ok(new ResponseBase<User>(result.GetSuccess()));
                }

                if (result.GetFailure().Any(x => x.Type.Equals("Critical")))
                {
                    return new ObjectResult(result.GetFailure()) { StatusCode = (int?)HttpStatusCode.InternalServerError };
                }

                return UnprocessableEntity(result.GetFailure());
            }
            catch (Exception e)
            {
                return new ObjectResult(_notificationManager.AddCritical(e.Message).GetNotifications()) { StatusCode = (int?)HttpStatusCode.InternalServerError };
            }
        }
    }
}
