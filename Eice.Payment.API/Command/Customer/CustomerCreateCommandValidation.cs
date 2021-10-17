using FluentValidation;

namespace Eice.Payment.API.Command
{
    public class CustomerCreateCommandValidation : AbstractValidator<CustomerCreateCommand>
    {
        public CustomerCreateCommandValidation()
        {
            //RuleFor(client => client.Cpf).NotEmpty();
            RuleFor(client => client.Cpf).Length(11);
            RuleFor(client => client.PartnerId).NotEmpty();
            RuleFor(client => client.AuthenticationKey).NotEmpty();
        }
    }
}
