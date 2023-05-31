using Eice.Payment.Domain.Notification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Eice.Payment.Domain.Oferta.Queries
{
    public class OfertaGetAllQueryHandler : QueryHandler<OfertaEntity>, IRequestHandler<OfertaGetAllQuery, IEnumerable<OfertaDto>>
    {
        public OfertaGetAllQueryHandler(IMediator bus, IOfertaQueryRepository queryRepository) : base(bus, queryRepository)
        {
        }

        public async Task<IEnumerable<OfertaDto>> Handle(OfertaGetAllQuery request, CancellationToken cancellationToken)
        {
            //buscar a moeda que está sendo oferecida e retornar no dto pelo partnerId do customercreated

            //if (!request.IsValid()) { GetNotificationsErrors(request); return default; }

            try
            {
                //filtrar por ofertas onde tem conta nos 2 partner
                //os 2 partners devem habilitar exchanges

                IEnumerable<OfertaEntity> list = await _queryRepository.GetAll();

                List<OfertaDto> resp = new();
                foreach (var item in list)
                {
                    resp.Add(new OfertaDto
                    {
                        Id = item.Id.ToString(),
                        CoinIdOffer = item.CoinOffer.Name,
                        QuantityOffer = item.QuantityOffer,
                        CoinIdReceive = item.CoinReceive.Name,
                        QuantityReceive = item.QuantityReceive,
                        Status = item.Status,
                        CreationTime = item.Id.CreationTime
                    });
                }

                return resp;
            }
            catch (Exception ex)
            {
                await _bus.Publish(new ExceptionNotification("036", ex.Message, null), cancellationToken);
                return default;
            }
        }
    }
}
