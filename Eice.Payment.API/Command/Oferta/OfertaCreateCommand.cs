using MediatR;

namespace Eice.Payment.API.Command.Oferta
{
    public class OfertaCreateCommand : Command, IRequest<string>
    {
        public string PartnerId { get; set; }
        public string CustomerIdCreated { get; set; }
        public decimal QuantityOffer { get; set; }
        public decimal QuantityReceive { get; set; }
        public string CoinIdReceive { get; set; }

        public override bool IsValid()
        {
            var validationResult = new OfertaCreateCommandValidation().Validate(this);
            return validationResult.IsValid;
        }
    }
}
