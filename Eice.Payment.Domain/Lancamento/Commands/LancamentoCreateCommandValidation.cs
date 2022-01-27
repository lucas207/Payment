using FluentValidation;

namespace Eice.Payment.Domain.Lancamento.Commands
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
