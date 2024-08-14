using ABCBrasil.Providers.BasicContractProvider.Lib;
using ABCBrasil.Providers.CommonProvider.Lib;
using Domain.Models.Dto.Requests;
using Domain.Models.Entities;

namespace Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<Try<List<NotificationBase>, User>> Create(UserRequest request);
        Task<Try<List<NotificationBase>, bool>> Delete(string id);
        Task<Try<List<NotificationBase>, User>> Get(string id);
        Task<Try<List<NotificationBase>, User>> Update(UserRequest request, string id);
    }
}
