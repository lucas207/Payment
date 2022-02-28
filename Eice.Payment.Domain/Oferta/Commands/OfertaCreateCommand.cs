using MediatR;

namespace Eice.Payment.Domain.Oferta.Commands
{
    public class OfertaCreateCommand : Domain.Command, IRequest<string>
    {
        public string PartnerId { get; set; }
        public string CustomerIdCreated { get; set; }
        public decimal QuantityOffer { get; set; }
        public decimal QuantityReceive { get; set; }
        public string CoinIdReceive { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new OfertaCreateCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
