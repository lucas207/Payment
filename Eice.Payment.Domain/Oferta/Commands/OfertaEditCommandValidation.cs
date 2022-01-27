using FluentValidation;

namespace Eice.Payment.Domain.Oferta.Commands
{
    public class OfertaEditCommandValidation : AbstractValidator<OfertaEditCommand>
    {
        public OfertaEditCommandValidation()
        {
            RuleFor(x => x.PartnerId).NotEmpty();
            RuleFor(x => x.OfertaId).NotEmpty();
            RuleFor(x => x.CustomerIdAccepted).NotEmpty();
        }
    }
}
