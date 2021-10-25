using FluentValidation;

namespace Eice.Payment.API.Command.Customer
{
    public class CustomerCreateCommandValidation : AbstractValidator<CustomerCreateCommand>
    {
        public CustomerCreateCommandValidation()
        {
            RuleFor(client => client.Cpf).Length(11);
            RuleFor(client => client.PartnerId).NotEmpty();
            RuleFor(client => client.AuthenticationKey).NotEmpty();
            //não pode mesmo cpf para mesmo partner
            //pode mesmo cpf diferentes partners
            //validar se essa authenticationkey é desse partner
        }
    }
}
