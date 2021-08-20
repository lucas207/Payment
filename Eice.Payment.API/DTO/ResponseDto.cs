using Eice.Payment.API.Notification;
using System.Collections.Generic;

namespace Eice.Payment.API.DTO
{
    public class ResponseDto<T> 
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public List<ExceptionNotification> Errors { get; internal set; }
    }
}
