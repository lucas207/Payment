using Eice.Payment.Domain.Notification;
using System.Collections.Generic;

namespace Eice.Payment.API.Response
{
    public class ResponseDto<T> 
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public List<ExceptionNotification> Errors { get; internal set; }
    }
}
