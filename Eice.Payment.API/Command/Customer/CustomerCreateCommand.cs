using MediatR;

namespace Eice.Payment.API.Command.Customer
{
    public class CustomerCreateCommand : Command, IRequest<string>
    {
        //retirar daqui pegar pelo auth
        public string PartnerId { get; set; }
        //retirar daqui pegar pelo auth
        public string AuthenticationKey { get; set; }
        public string Cpf { get; set; }

        public override bool IsValid()
        {
            var validationResult = new CustomerCreateCommandValidation().Validate(this);
            return validationResult.IsValid;
        }
    }
}
