using Eice.Payment.Domain.Notification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Eice.Payment.Domain.Partner.Queries
{
    public class PartnerGetAllQueryHandler : QueryHandler<PartnerEntity>, IRequestHandler<PartnerGetAllQuery, IEnumerable<PartnerDto>>
    {
        private readonly IPartnerQueryRepository _partnerRepository;

        public PartnerGetAllQueryHandler(IMediator bus, IPartnerQueryRepository partnerRepository) : base(bus, partnerRepository)
        {
            _partnerRepository = partnerRepository;
        }

        public async Task<IEnumerable<PartnerDto>> Handle(PartnerGetAllQuery request, CancellationToken cancellationToken)
        {
            //if (!request.IsValid()) { GetNotificationsErrors(request); return default; }

            try
            {
                //filter por habilitou negociações das moedas virtuais
                var list = await _partnerRepository.GetAllEnableExchange();

                List<PartnerDto> resp = new();
                foreach (var item in list)
                {
                    resp.Add(new PartnerDto
                    {
                        Id = item.Id.ToString(),
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
                await _bus.Publish(new ExceptionNotification("047", ex.Message, null, ex.StackTrace), cancellationToken);
                return default;
            }
        }
    }
}
