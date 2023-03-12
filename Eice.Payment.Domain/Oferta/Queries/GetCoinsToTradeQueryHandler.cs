using Eice.Payment.Domain.Customer.Queries;
using Eice.Payment.Domain.Notification;
using Eice.Payment.Domain.Partner;
using Eice.Payment.Domain.Partner.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Eice.Payment.Domain.Oferta.Queries
{
    public class GetCoinsToTradeQueryHandler : QueryHandler<PartnerEntity>, IRequestHandler<GetCoinsToTradeQuery, IEnumerable<CoinDto>>
    {
        private readonly IPartnerQueryRepository _partnerRepository;
        private readonly ICustomerQueryRepository _customerQueryRepository;

        public GetCoinsToTradeQueryHandler(IMediator bus, IPartnerQueryRepository queryRepository,
            ICustomerQueryRepository customerQueryRepository) : base(bus, queryRepository)
        {
            _partnerRepository = queryRepository;
            _customerQueryRepository = customerQueryRepository;
        }

        public async Task<IEnumerable<CoinDto>> Handle(GetCoinsToTradeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                //filter por habilitou negociações
                var list = await _partnerRepository.GetAllEnableExchange();

                var customer = await _customerQueryRepository.Get(request.CustomerId);
                //Obter contas do customer de todos partners
                var contas = _customerQueryRepository.GetAllByCpf(customer.Cpf).Select(x => x.PartnerId).ToList();
                //filtro partners habilitados e cliente tem conta
                var partners = list.Where(x => contas.Contains(x.Id.ToString()));

                List<CoinDto> resp = new();
                foreach (var item in partners)
                {
                    resp.Add(new CoinDto
                    {
                        Id = item.Id.ToString(),
                        Name = item.CoinName,
                        Image = item.Image
                    });
                }

                return resp;
            }
            catch (Exception ex)
            {
                await _bus.Publish(new ExceptionNotification("047", ex.Message, null), cancellationToken);
                return default;
            }
        }
    }
}
