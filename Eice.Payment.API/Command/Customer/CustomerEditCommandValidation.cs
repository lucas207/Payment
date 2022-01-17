using FluentValidation;

namespace Eice.Payment.API.Command.Customer
{
    public class CustomerEditCommandValidation : AbstractValidator<CustomerEditCommand>
    {
        public CustomerEditCommandValidation()
        {
            RuleFor(client => client.PartnerId).NotEmpty();
            RuleFor(client => client.Id).NotEmpty();
            RuleFor(client => client.Name).NotEmpty();
        }
    }
}
