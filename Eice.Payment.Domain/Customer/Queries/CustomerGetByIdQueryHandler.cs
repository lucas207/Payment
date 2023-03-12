using Eice.Payment.Domain.Lancamento;
using Eice.Payment.Domain.Lancamento.Queries;
using Eice.Payment.Domain.Notification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Eice.Payment.Domain.Customer.Queries
{
    public class CustomerGetByIdQueryHandler : QueryHandler<CustomerEntity>, IRequestHandler<CustomerGetByIdQuery, CustomerDetailDto>
    {
        private readonly ICustomerQueryRepository _customerQueryRepository;

        public CustomerGetByIdQueryHandler(IMediator bus, ICustomerQueryRepository customerRepository) : base(bus, customerRepository)
        {
            _customerQueryRepository = customerRepository;
        }

        public async Task<CustomerDetailDto> Handle(CustomerGetByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                CustomerEntity customer = await _customerQueryRepository.Get(request.Id);

                return new CustomerDetailDto
                {
                    Id = customer.Id.ToString(),
                    PartnerId = customer.PartnerId,
                    Cpf = customer.Cpf,
                    Name = customer.Name,
                    Saldo = customer.SaldoAtual,
                    CreationTime = customer.Date,
                    Lancamentos = LancamentosToDto(customer.Lancamentos)
                };
            }
            catch (Exception ex)
            {
                await _bus.Publish(new ExceptionNotification("016", ex.Message, null), cancellationToken);
                return default;
            }
        }

        //Criar um extensions das entidades?
        private static IList<LancamentoDto> LancamentosToDto(List<LancamentoEntity> lancamentos)
        {
            List<LancamentoDto> resp = new();
            foreach (var item in lancamentos.OrderByDescending(x => x.Id.CreationTime))
            {
                resp.Add(new LancamentoDto
                {
                    Description = item.Description,
                    Quantidade = item.Quantity,
                    Id = item.Id.ToString(),
                    CreationTime = item.Id.CreationTime
                });
            }

            return resp;
        }
    }
}
