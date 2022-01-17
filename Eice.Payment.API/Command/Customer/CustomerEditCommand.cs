using MediatR;

namespace Eice.Payment.API.Command.Customer
{
    public class CustomerEditCommand : Command, IRequest<bool>
    {
        public string PartnerId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }

        public override bool IsValid()
        {
            var validationResult = new CustomerEditCommandValidation().Validate(this);
            return validationResult.IsValid;
        }
    }
}
