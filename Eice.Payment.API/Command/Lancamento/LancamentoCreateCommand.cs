using MediatR;
using System;

namespace Eice.Payment.API.Command.Lancamento
{
    public class LancamentoCreateCommand : Command, IRequest<string>
    {
        public string CustomerId { get; set; }
        public int Quantity { get; set; }

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
