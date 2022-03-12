using MediatR;

namespace Eice.Payment.Domain.Partner.Queries
{
    public class PartnerGetByIdQuery : Query, IRequest<PartnerDto>
    {
        public string Id { get; set; }
    }
}
