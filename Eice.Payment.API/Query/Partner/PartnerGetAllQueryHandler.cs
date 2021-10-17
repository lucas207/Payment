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
    public class PartnerGetAllQueryHandler : QueryHandler, IRequestHandler<PartnerGetAllQuery, IEnumerable<PartnerDto>>
    {
        private readonly IPartnerQueryRepository _partnerRepository;

        public PartnerGetAllQueryHandler(IMediator bus, IPartnerQueryRepository partnerRepository) : base(bus)
        {
            _partnerRepository = partnerRepository;
        }

        public async Task<IEnumerable<PartnerDto>> Handle(PartnerGetAllQuery request, CancellationToken cancellationToken)
        {
            //if (!request.IsValid()) { GetNotificationsErrors(request); return default; }

            try
            {
                var list = await _partnerRepository.GetAll();

                List<PartnerDto> resp = new List<PartnerDto>();
                foreach (var item in list)
                {
                    resp.Add(new PartnerDto
                    {
                        Id = item.Id.ToString(),
                        Cnpj = item.Cnpj,
                        Name = item.Name,
                        CreationTime = item.Id.CreationTime
                    });
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
