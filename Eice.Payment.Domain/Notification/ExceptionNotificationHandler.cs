using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Eice.Payment.Domain.Notification
{
    public class ExceptionNotificationHandler : INotificationHandler<ExceptionNotification>
    {
        private List<ExceptionNotification> _notifications;

        public ExceptionNotificationHandler()
        {
            _notifications = new List<ExceptionNotification>();
        }

        public Task Handle(ExceptionNotification notification, CancellationToken cancellationToken)
        {
            _notifications.Add(notification);

            return Task.CompletedTask;
        }

        public virtual List<ExceptionNotification> GetNotifications()
            => _notifications;

        public virtual bool HasNotifications()
            => GetNotifications().Any();

        public void Dispose()
            => _notifications = new List<ExceptionNotification>();

    }
}
