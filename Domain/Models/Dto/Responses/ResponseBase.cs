using ABCBrasil.Providers.BasicContractProvider.Lib;

namespace Domain.Models.Dto.Responses
{
    public class ResponseBase<T> where T : class
    {
        public ResponseBase(T data)
        {
            Data = data;
        }

        public ResponseBase(List<NotificationBase> errors)
        {
            Errors = errors;
        }

        public bool Success { get { return !Errors.Any(); } }
        public List<NotificationBase> Errors { get; set; } = new();
        public T? Data { get; set; }
    }
}