using MediatR;

namespace Eice.Payment.API.Query.Partner
{
    public class PartnerAuthenticateQuery : Query, IRequest<string>
    {
        public string AuthenticationKey { get; set; }
    }
}
