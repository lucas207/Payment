using FluentValidation;

namespace Eice.Payment.API.Command.Lancamento
{
    public class LancamentoCreateCommandValidation : AbstractValidator<LancamentoCreateCommand>
    {
        public LancamentoCreateCommandValidation()
        {
            RuleFor(client => client.PartnerId).NotEmpty();
            RuleFor(client => client.CustomerId).NotEmpty();
            RuleFor(client => client.Quantity != 0);
        }
    }
}
