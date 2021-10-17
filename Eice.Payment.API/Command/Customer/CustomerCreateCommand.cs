using MediatR;

namespace Eice.Payment.API.Command
{
    public class CustomerCreateCommand : Command, IRequest<string>
    {
        public string PartnerId { get; set; }
        public string AuthenticationKey { get; set; }
        public string Cpf { get; set; }

        public override bool IsValid()
        {
            var validationResult = new CustomerCreateCommandValidation().Validate(this);
            return validationResult.IsValid;
        }
    }
}
