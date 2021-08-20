using Eice.Payment.API.Notification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eice.Payment.API.Command
{
    public class CommandHandler
    {
        protected readonly IMediator _bus;

        public CommandHandler(IMediator bus)
        {
            _bus = bus;
        }
        public void GetNotificationsErrors(Command command)
        {
            foreach (var erro in command.GetValidationResult().Errors)
            {
                _bus.Publish(new ExceptionNotification(erro.ErrorCode, erro.ErrorMessage, erro.PropertyName, new Exception().StackTrace));
            }
        }
    }
}
