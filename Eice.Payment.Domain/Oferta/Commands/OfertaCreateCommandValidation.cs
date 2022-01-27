using FluentValidation;

namespace Eice.Payment.Domain.Oferta.Commands
{
    public class OfertaCreateCommandValidation : AbstractValidator<OfertaCreateCommand>
    {
        public OfertaCreateCommandValidation()
        {
            RuleFor(x => x.PartnerId).NotEmpty();
            RuleFor(x => x.CustomerIdCreated).NotEmpty();
            RuleFor(x => x.QuantityOffer).NotEmpty();
            RuleFor(x => x.QuantityReceive).NotEmpty();
            RuleFor(x => x.CoinIdReceive).NotEmpty();
        }
    }
}
