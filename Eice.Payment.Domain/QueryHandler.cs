using MediatR;

namespace Eice.Payment.Domain
{
    public class QueryHandler<T>
    {
        protected readonly IMediator _bus;
        protected readonly IQueryRepository<T> _queryRepository;

        public QueryHandler(IMediator bus, IQueryRepository<T> queryRepository)
        {
            _bus = bus;
            _queryRepository = queryRepository;
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
