using MediatR;

namespace Eice.Payment.Domain.Lancamento.Commands
{
    public class LancamentoCreateCommand : Domain.Command, IRequest<bool>
    {
        public string PartnerId { get; set; }
        public string CustomerId { get; set; }
        public decimal Quantity { get; set; }
        public string Description { get; set; }

        public override bool IsValid()
        {
            var validationResult = new LancamentoCreateCommandValidation().Validate(this);
            return validationResult.IsValid;
        }
    }
}
