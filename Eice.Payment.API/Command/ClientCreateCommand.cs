using Eice.Payment.Domain.Client;
using MediatR;
using System;

namespace Eice.Payment.API.Command
{
    public class ClientCreateCommand : Command, IRequest<Guid>
    {
        public string Name { get; set; }
        public int CpfCnpj { get; set; }
        public ETipoPessoa TipoPessoa { get; set; }

        public override bool IsValid()
        {
            var validationResult = new ClientCreateCommandValidation().Validate(this);
            return validationResult.IsValid;
        }
    }
}
