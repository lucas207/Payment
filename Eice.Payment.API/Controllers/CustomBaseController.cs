using Eice.Payment.API.Response;
using Eice.Payment.Domain.Notification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Eice.Payment.API.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        protected readonly IMediator _mediator;
        private readonly ExceptionNotificationHandler _notifications;

        protected IEnumerable<ExceptionNotification> Notifications => _notifications.GetNotifications();

        protected CustomBaseController(IMediator mediator, INotificationHandler<ExceptionNotification> notifications)
        {
            _mediator = mediator;
            _notifications = (ExceptionNotificationHandler)notifications;
        }

        protected bool IsValidOperation()
            => !_notifications.HasNotifications();

        protected IActionResult ResponseHandle(IActionResult action)
        {
            if (IsValidOperation())
                return action;

            return BadRequest(new ResponseDto<object>
            {
                Success = false,
                Errors = _notifications.GetNotifications()
            });
        }
    }
}
