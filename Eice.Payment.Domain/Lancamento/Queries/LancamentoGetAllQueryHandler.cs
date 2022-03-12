using Eice.Payment.Domain.Customer;
using Eice.Payment.Domain.Customer.Queries;
using Eice.Payment.Domain.Notification;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Eice.Payment.Domain.Lancamento.Queries
{
    public class LancamentoGetAllQueryHandler : QueryHandler<CustomerEntity>, IRequestHandler<LancamentoGetAllQuery, LancamentoDto>
    {
        public LancamentoGetAllQueryHandler(IMediator bus, ICustomerQueryRepository queryRepository) : base(bus, queryRepository)
        {
        }

        public async Task<LancamentoDto> Handle(LancamentoGetAllQuery request, CancellationToken cancellationToken)
        {
            //if (!request.IsValid()) { GetNotificationsErrors(request); return default; }

            try
            {
                LancamentoDto resp = new LancamentoDto();
                CustomerEntity customerFinded = await _queryRepository.Get(request.CustomerId);

                if (customerFinded is null)
                    throw new Exception("Customer not find");

                resp.Saldo = customerFinded.SaldoAtual;
                foreach (var item in customerFinded.Lancamentos.OrderByDescending(x => x.Id.CreationTime))
                {
                    resp.LancamentoItems.Add(new LancamentoItemDto
                    {
                        Description = item.Description,
                        Quantidade = item.Quantity,
                        CreationTime = item.Id.CreationTime
                    });
                }

                return resp;
            }
            catch (Exception ex)
            {
                await _bus.Publish(new ExceptionNotification("026", ex.Message, null), cancellationToken);
                return default;
            }
        }
    }
}
