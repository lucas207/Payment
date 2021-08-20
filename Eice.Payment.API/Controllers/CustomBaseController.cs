using Eice.Payment.API.Notification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eice.Payment.API.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        private readonly ExceptionNotificationHandler _notifications;

        protected IEnumerable<ExceptionNotification> Notifications => _notifications.GetNotifications();

        protected CustomBaseController(INotificationHandler<ExceptionNotification> notifications)
        {
            _notifications = (ExceptionNotificationHandler)notifications;
        }

        protected bool IsValidOperation()
            => !_notifications.HasNotifications();

        protected async Task<IActionResult> ResponseAsync(IActionResult action)
        {
            if (IsValidOperation())
                return action;

            return BadRequest(new
            {
                success = false,
                errors = _notifications.GetNotifications()
            });
        }
    }
}
