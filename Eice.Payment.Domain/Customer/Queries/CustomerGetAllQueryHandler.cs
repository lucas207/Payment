using Eice.Payment.Domain.Notification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Eice.Payment.Domain.Customer.Queries
{
    public class CustomerGetAllQueryHandler : QueryHandler<CustomerEntity>, IRequestHandler<CustomerGetAllQuery, IEnumerable<CustomerDto>>
    {
        private readonly ICustomerQueryRepository _customerQueryRepository;

        public CustomerGetAllQueryHandler(IMediator bus, ICustomerQueryRepository customerRepository) : base(bus, customerRepository)
        {
            _customerQueryRepository = customerRepository;
        }

        public async Task<IEnumerable<CustomerDto>> Handle(CustomerGetAllQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<CustomerEntity> list = _customerQueryRepository.GetAllFromPartnerId(request.PartnerId);

                List<CustomerDto> resp = new();
                foreach (var item in list)
                {
                    resp.Add(new CustomerDto
                    {
                        Id = item.Id.ToString(),
                        PartnerId = item.PartnerId,
                        Cpf = item.Cpf,
                        Name = item.Name,
                        Saldo = item.SaldoAtual,
                        CreationTime = item.Id.CreationTime
                    });
                }

                return resp;
            }
            catch (Exception ex)
            {
                await _bus.Publish(new ExceptionNotification("016", ex.Message, null, ex.StackTrace), cancellationToken);
                return default;
            }
        }
    }
}
