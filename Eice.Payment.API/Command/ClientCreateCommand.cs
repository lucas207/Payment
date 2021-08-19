using Eice.Payment.Infra.Model;
using MediatR;
using System;

namespace Eice.Payment.API.Command
{
    public class ClientCreateCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public int CpfCnpj { get; set; }
        public ETipoPessoa TipoPessoa { get; set; }
    }
}
