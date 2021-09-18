using Eice.Payment.API.Notification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eice.Payment.API.Query
{
    public class QueryHandler
    {
        protected readonly IMediator _bus;

        public QueryHandler(IMediator bus)
        {
            _bus = bus;
        }

        //por enquanto não
        //public void GetNotificationsErrors(Query query)
        //{
        //    foreach (var erro in query.GetValidationResult().Errors)
        //    {
        //        _bus.Publish(new ExceptionNotification(erro.ErrorCode, erro.ErrorMessage, erro.PropertyName, new Exception().StackTrace));
        //    }
        //}
    }
}
