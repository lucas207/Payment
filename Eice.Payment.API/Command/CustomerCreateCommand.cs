using Eice.Payment.Domain.Customer;
using MediatR;

namespace Eice.Payment.API.Command
{
    public class CustomerCreateCommand : Command, IRequest<string>
    {
        public string Name { get; set; }
        public string CpfCnpj { get; set; }
        public ETipoPessoa TipoPessoa { get; set; }

        public override bool IsValid()
        {
            var validationResult = new CustomerCreateCommandValidation().Validate(this);
            return validationResult.IsValid;
        }
    }
}
