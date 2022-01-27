using Eice.Payment.Domain.Partner;

namespace Eice.Payment.Domain.Authentication
{
    public interface ITokenService
    {
        string GenerateToken(PartnerEntity user);
    }
}
