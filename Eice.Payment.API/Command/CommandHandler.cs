using Eice.Payment.API.Notification;
using Eice.Payment.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eice.Payment.API.Command
{
    public class CommandHandler<T>
    {
        protected readonly IMediator _bus;
        protected readonly ICommandRepository<T> _commandRepository;

        public CommandHandler(IMediator bus, ICommandRepository<T> commandRepository)
        {
            _bus = bus;
            _commandRepository = commandRepository;
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
