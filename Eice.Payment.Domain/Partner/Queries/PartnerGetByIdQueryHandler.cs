using Eice.Payment.Domain.Notification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Eice.Payment.Domain.Partner.Queries
{
    public class PartnerGetByIdQueryHandler : QueryHandler<PartnerEntity>, IRequestHandler<PartnerGetByIdQuery, PartnerDto>
    {
        private readonly IPartnerQueryRepository _partnerRepository;

        public PartnerGetByIdQueryHandler(IMediator bus, IPartnerQueryRepository queryRepository) : base(bus, queryRepository)
        {
            _partnerRepository = queryRepository;
        }

        public async Task<PartnerDto> Handle(PartnerGetByIdQuery request, CancellationToken cancellationToken)
        {
            //if (!request.IsValid()) { GetNotificationsErrors(request); return default; }

            try
            {
                var item = await _partnerRepository.Get(request.Id);

                var resp = new PartnerDto
                {
                    Id = item.Id.ToString(),
                    Cnpj = item.Cnpj,
                    Name = item.Name,
                    CoinName = item.CoinName,
                    NameAlias = item.NameAlias,
                    Image = item.Image
                };

                return resp;
            }
            catch (Exception ex)
            {
                await _bus.Publish(new ExceptionNotification("048", ex.Message, null), cancellationToken);
                return default;
            }
        }
    }
}
