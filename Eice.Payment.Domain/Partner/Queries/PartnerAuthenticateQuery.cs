using MediatR;

namespace Eice.Payment.Domain.Partner.Queries
{
    public class PartnerAuthenticateQuery : Query, IRequest<string>
    {
        public string AuthenticationKey { get; set; }
    }
}
