using Eice.Payment.Domain.Client;
using MediatR;

namespace Eice.Payment.API.Command
{
    public class ClientCreateCommand : Command, IRequest<string>
    {
        public string Name { get; set; }
        public string CpfCnpj { get; set; }
        public ETipoPessoa TipoPessoa { get; set; }

        public override bool IsValid()
        {
            var validationResult = new ClientCreateCommandValidation().Validate(this);
            return validationResult.IsValid;
        }
    }
}
