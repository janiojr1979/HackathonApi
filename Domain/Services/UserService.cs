using ABCBrasil.Providers.BasicContractProvider.Lib;
using ABCBrasil.Providers.CommonProvider.Lib;
using AutoMapper;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Domain.Models.Dto.Requests;
using Domain.Models.Entities;

namespace Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryBase _repositoryBase;
        private readonly INotificationManager _notificationManager;
        private readonly IMapper _mapper;

        public UserService(INotificationManager notificationManager, IMapper mapper, IRepositoryBase repositoryBase)
        {
            _notificationManager = notificationManager;
            _repositoryBase = repositoryBase;
            _mapper = mapper;
        }

        /// <summary>
        /// Cria um novo usuário
        /// </summary>
        /// <param name="request">Dados do usuário</param>
        /// <returns>Usuário criado</returns>
        public async Task<Try<List<NotificationBase>, User>> Create(UserRequest request)
        {
            try
            {
                if (request == null)
                {
                    return _notificationManager.AddError("Request para criação de usuário está vazio.").GetNotifications();
                }

                var user = _mapper.Map<User>(request);
                var resultCreate = await _repositoryBase.Insert(user, user.Id);

                return resultCreate.IsSuccess && resultCreate.GetSuccess() ? user : resultCreate.GetFailure();
            }
            catch (Exception e)
            {
                return _notificationManager.AddCritical(e.Message).GetNotifications();
            }
        }

        /// <summary>
        /// Deleta usuário
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <returns>Retona um boleano para o resultado da ação. true, excluído com sucesso e false para não excluído.</code></returns>
        public async Task<Try<List<NotificationBase>, bool>> Delete(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    return _notificationManager.AddError("Id inválido.").GetNotifications();
                }

                var resultCreate = await _repositoryBase.Delete(id);

                return resultCreate.IsSuccess && resultCreate.GetSuccess() ? true : resultCreate.GetFailure();
            }
            catch (Exception e)
            {
                return _notificationManager.AddCritical(e.Message).GetNotifications();
            }
        }

        /// <summary>
        /// Consulta usuário pelo identificador.
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <returns>Usuário</returns>
        public async Task<Try<List<NotificationBase>, User>> Get(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    return _notificationManager.AddError("Id inválido.").GetNotifications();
                }

                var resultCreate = await _repositoryBase.GetById<User>(id);

                return resultCreate.IsSuccess ? resultCreate.GetSuccess() : resultCreate.GetFailure();
            }
            catch (Exception e)
            {
                return _notificationManager.AddCritical(e.Message).GetNotifications();
            }
        }

        /// <summary>
        /// Atualiza dados do usuário.
        /// </summary>
        /// <param name="request">Dados a serem atualizados</param>
        /// <param name="id">Identificador do usuário</param>
        /// <returns>Retorna usuário consultado</returns>
        public async Task<Try<List<NotificationBase>, User>> Update(UserRequest request, string id)
        {
            try
            {
                if (request == null)
                {
                    return _notificationManager.AddError("Request para criação de usuário está vazio.").GetNotifications();
                }

                var user = _mapper.Map<User>(request);
                user.Id = id;
                var resultCreate = await _repositoryBase.Update(user, user.Id);

                return resultCreate.IsSuccess && resultCreate.GetSuccess() ? user : resultCreate.GetFailure();
            }
            catch (Exception e)
            {
                return _notificationManager.AddCritical(e.Message).GetNotifications();
            }
        }
    }
}