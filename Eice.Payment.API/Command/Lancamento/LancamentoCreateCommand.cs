using MediatR;
using System;

namespace Eice.Payment.API.Command.Lancamento
{
    public class LancamentoCreateCommand : Command, IRequest<bool>
    {
        public string CustomerId { get; set; }
        public string PartnerId { get; set; }
        public string AuthenticationKey { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
