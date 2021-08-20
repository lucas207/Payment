using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eice.Payment.API.Notification
{
    public class ExceptionNotification : INotification
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string ParamName { get; set; }
        public string Stack { get; set; }
        public DateTime DateTime { get; set; }

        public ExceptionNotification(string code, string message, string paramName = null, string stack = null)
        {
            Code = code;
            Message = message;
            ParamName = paramName;
            Stack = stack;
            DateTime = DateTime.Now;
        }
    }
}
