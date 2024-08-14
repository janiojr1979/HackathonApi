using ABCBrasil.Providers.BasicContractProvider.Lib;
using ABCBrasil.Providers.CommonProvider.Lib;
using Domain.Interfaces.Repository;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System.Linq.Expressions;

namespace Infra.Repository
{
    public class RepositoryBase : IRepositoryBase
    {
        protected readonly IDocumentStore _ravenDbProvider;
        protected readonly INotificationProvider _notificationProvider;

        public RepositoryBase(IDocumentStore ravenDbProvider, INotificationProvider notificationProvider)
        {
            _ravenDbProvider = ravenDbProvider;
            _notificationProvider = notificationProvider;
        }

        public async Task<Try<List<NotificationBase>, T>> GetById<T>(string id)
        {
            INotificationManager notifications = _notificationProvider.CreateNotification();

            try
            {
                T result;

                using (IAsyncDocumentSession session = _ravenDbProvider.OpenAsyncSession())
                {
                    result = await session.LoadAsync<T>(id);
                }

                return result;
            }
            catch (Exception ex)
            {
                notifications.AddError(ex.ToString());
                return notifications.GetNotifications();
            }
        }

        public async Task<Try<List<NotificationBase>, bool>> Insert<T>(T command, string documentId)
        {
            INotificationManager notifications = _notificationProvider.CreateNotification();

            try
            {
                bool result;

                using (IAsyncDocumentSession session = _ravenDbProvider.OpenAsyncSession())
                {
                    await session.StoreAsync(command, changeVector: string.Empty, documentId);
                    await session.SaveChangesAsync();

                    result = true;
                }

                return result;
            }
            catch (Exception ex)
            {
                notifications.AddError($"Erro ao inserir no ravendb: {ex.Message}");
                return notifications.GetNotifications();
            }
        }

        public async Task<Try<List<NotificationBase>, bool>> Delete(string documentId)
        {
            INotificationManager notifications = _notificationProvider.CreateNotification();

            try
            {
                bool result;

                using (IAsyncDocumentSession session = _ravenDbProvider.OpenAsyncSession())
                {
                    session.Delete(documentId);
                    await session.SaveChangesAsync();

                    result = true;
                }

                return result;
            }
            catch (Exception ex)
            {
                notifications.AddError($"Erro ao inserir no ravendb: {ex.Message}");
                return notifications.GetNotifications();
            }
        }

        public async Task<Try<List<NotificationBase>, bool>> InsertDocuments<T>(T command, string commandId)
        {
            INotificationManager notifications = _notificationProvider.CreateNotification();

            try
            {
                bool result;

                using (IAsyncDocumentSession session = _ravenDbProvider.OpenAsyncSession())
                {
                    await session.StoreAsync(command, changeVector: string.Empty, commandId);
                    await session.SaveChangesAsync();
                    result = true;
                }

                return result;
            }
            catch (Exception ex)
            {
                notifications.AddError($"Erro ao inserir no ravendb: {ex.Message}");
                return notifications.GetNotifications();
            }
        }

        public async Task<Try<List<NotificationBase>, bool>> Update<T>(T command, string documentId)
        {
            INotificationManager notifications = _notificationProvider.CreateNotification();
            try
            {
                using (IAsyncDocumentSession session = _ravenDbProvider.OpenAsyncSession())
                {
                    await session.StoreAsync(command, changeVector: null, documentId);
                    await session.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                notifications.AddError($"Erro ao atualizar no ravendb: {ex.Message}");
                return notifications.GetNotifications();
            }
        }

        public async Task<Try<List<NotificationBase>, List<T>>> GetAllWhereAsync<T>(Expression<Func<T, bool>> predicate)
        {
            INotificationManager notifications = _notificationProvider.CreateNotification();

            try
            {
                List<T> result;

                using (IAsyncDocumentSession session = _ravenDbProvider.OpenAsyncSession())
                {
                    result = await session.Query<T>().Where(predicate).ToListAsync().ConfigureAwait(false);
                }

                return result;
            }
            catch (Exception ex)
            {
                notifications.AddError($"Erro ao consultar lista no ravendb: {ex.Message}");
                return notifications.GetNotifications();
            }
        }
    }
}
