using Eice.Payment.Domain.Notification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Eice.Payment.Domain.Oferta.Queries
{
    internal class OfertaGetByCustomerQueryHandler : QueryHandler<OfertaEntity>, IRequestHandler<OfertaGetByCustomerQuery, IEnumerable<OfertaDto>>
    {
        private readonly IOfertaQueryRepository _ofertaQueryRepository;

        public OfertaGetByCustomerQueryHandler(IMediator bus, IOfertaQueryRepository queryRepository) : base(bus, queryRepository)
        {
            _ofertaQueryRepository = queryRepository;
        }

        public async Task<IEnumerable<OfertaDto>> Handle(OfertaGetByCustomerQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<OfertaEntity> list = _ofertaQueryRepository.GetByCustomer(request.CustomerId);

                if (request.Status != null)
                    list = list.Where(x => x.Status == request.Status);

                List<OfertaDto> resp = new();
                foreach (OfertaEntity entity in list)
                {
                    resp.Add(new OfertaDto
                    {
                        Id = entity.Id.ToString(),
                        CoinIdOffer = entity.CoinOffer.Id.ToString(),
                        CoinNameOffer = entity.CoinOffer.Name,
                        CoinIdReceive = entity.CoinReceive.Id.ToString(),
                        CoinNameReceive = entity.CoinReceive.Name,
                        QuantityOffer = entity.QuantityOffer,
                        QuantityReceive = entity.QuantityReceive,
                        Status = entity.Status,
                        CreationTime = entity.Id.CreationTime
                    });
                }

                return resp.OrderByDescending(x => x.CreationTime);
            }
            catch (Exception ex)
            {
                await _bus.Publish(new ExceptionNotification("037", ex.Message, null), cancellationToken);
                return default;
            }
        }
    }
}
