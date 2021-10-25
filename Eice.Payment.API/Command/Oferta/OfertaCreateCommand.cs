using MediatR;

namespace Eice.Payment.API.Command.Oferta
{
    public class OfertaCreateCommand : Command, IRequest<string>
    {
        //retirar daqui pegar pelo auth
        public string PartnerId { get; set; }
        //retirar daqui pegar pelo auth
        public string AuthenticationKey { get; set; }

        public string CustomerIdCreated { get; set; }
        public int QuantityOffer { get; set; }
        public int QuantityReceive { get; set; }
        public string CoinIdReceive { get; set; }

        public override bool IsValid()
        {
            var validationResult = new OfertaCreateCommandValidation().Validate(this);
            return validationResult.IsValid;
        }
    }
}
