using Eice.Payment.API.DTO;
using Eice.Payment.API.Notification;
using Eice.Payment.Domain.Customer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Eice.Payment.API.Query
{
    public class CustomerGetAllQueryHandler : QueryHandler, IRequestHandler<CustomerGetAllQuery, IEnumerable<CustomerDto>>
    {
        private readonly ICustomerQueryRepository _customerRepository;

        public CustomerGetAllQueryHandler(IMediator bus, ICustomerQueryRepository customerRepository) : base(bus)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<CustomerDto>> Handle(CustomerGetAllQuery request, CancellationToken cancellationToken)
        {
            //if (!request.IsValid()) { GetNotificationsErrors(request); return default; }

            try
            {
                var list = await _customerRepository.GetAll();

                List<CustomerDto> resp = new List<CustomerDto>();
                foreach (var item in list)
                {
                    resp.Add(new CustomerDto { Id = item.Id.ToString(), CpfCnpj = item.CpfCnpj, Name = item.Name, TipoPessoa = item.TipoPessoa });
                }

                return resp;
            }
            catch (Exception ex)
            {
                await _bus.Publish(new ExceptionNotification("500", ex.Message, null, ex.StackTrace));
                return default;
            }
        }
    }
}
