using Eice.Payment.API.DTO;
using Eice.Payment.API.Notification;
using Eice.Payment.Domain.Customer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Eice.Payment.API.Query.Customer
{
    public class CustomerGetAllQueryHandler : QueryHandler<CustomerEntity>, IRequestHandler<CustomerGetAllQuery, IEnumerable<CustomerDto>>
    {
        public CustomerGetAllQueryHandler(IMediator bus, ICustomerQueryRepository customerRepository) : base(bus, customerRepository)
        {
        }

        public async Task<IEnumerable<CustomerDto>> Handle(CustomerGetAllQuery request, CancellationToken cancellationToken)
        {
            //if (!request.IsValid()) { GetNotificationsErrors(request); return default; }

            try
            {
                IEnumerable<CustomerEntity> list = await _queryRepository.GetAll();

                List<CustomerDto> resp = new List<CustomerDto>();
                foreach (var item in list)
                {
                    resp.Add(new CustomerDto
                    {
                        Id = item.Id.ToString(),
                        Cpf = item.Cpf,
                        PartnerId = item.PartnerId,
                        CreationTime = item.Id.CreationTime
                    });
                }

                return resp;
            }
            catch (Exception ex)
            {
                await _bus.Publish(new ExceptionNotification("500", ex.Message, null, ex.StackTrace), cancellationToken);
                return default;
            }
        }
    }
}
