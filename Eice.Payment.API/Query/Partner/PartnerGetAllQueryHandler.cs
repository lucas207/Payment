using Eice.Payment.API.DTO;
using Eice.Payment.API.Notification;
using Eice.Payment.Domain.Partner;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Eice.Payment.API.Query.Partner
{
    public class PartnerGetAllQueryHandler : QueryHandler<PartnerEntity>, IRequestHandler<PartnerGetAllQuery, IEnumerable<PartnerDto>>
    {
        public PartnerGetAllQueryHandler(IMediator bus, IPartnerQueryRepository partnerRepository) : base(bus, partnerRepository)
        {
        }

        public async Task<IEnumerable<PartnerDto>> Handle(PartnerGetAllQuery request, CancellationToken cancellationToken)
        {
            //if (!request.IsValid()) { GetNotificationsErrors(request); return default; }

            try
            {
                var list = await _queryRepository.GetAll();

                List<PartnerDto> resp = new();
                foreach (var item in list)
                {
                    resp.Add(new PartnerDto
                    {
                        Cnpj = item.Cnpj,
                        Name = item.Name,
                        CoinName = item.CoinName,
                        NameAlias = item.NameAlias,
                        Image = item.Image
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
