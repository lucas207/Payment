using FluentValidation;

namespace Eice.Payment.API.Command
{
    public class CustomerCreateCommandValidation : AbstractValidator<CustomerCreateCommand>
    {
        public CustomerCreateCommandValidation()
        {
            RuleFor(client => client.Name).NotNull();
            //VALIDAR FORMATO CPFCNPJ
        }
    }
}
