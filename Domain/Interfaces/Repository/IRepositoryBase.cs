using ABCBrasil.Providers.BasicContractProvider.Lib;
using ABCBrasil.Providers.CommonProvider.Lib;
using System.Linq.Expressions;

namespace Domain.Interfaces.Repository
{
    public interface IRepositoryBase
    {
        Task<Try<List<NotificationBase>, bool>> Insert<T>(T command, string documentId);
        Task<Try<List<NotificationBase>, bool>> Delete(string documentId);
        Task<Try<List<NotificationBase>, bool>> InsertDocuments<T>(T command, string commandId);
        Task<Try<List<NotificationBase>, T>> GetById<T>(string id);
        Task<Try<List<NotificationBase>, bool>> Update<T>(T command, string documentId);
        Task<Try<List<NotificationBase>, List<T>>> GetAllWhereAsync<T>(Expression<Func<T, bool>> predicate);
    }
}
