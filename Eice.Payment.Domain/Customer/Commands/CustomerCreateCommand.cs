using MediatR;

namespace Eice.Payment.Domain.Customer.Commands
{
    public class CustomerCreateCommand : Eice.Payment.Domain.Command, IRequest<string>
    {
        public string PartnerId { get; set; }
        public string Cpf { get; set; }
        public string Name { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new CustomerCreateCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
