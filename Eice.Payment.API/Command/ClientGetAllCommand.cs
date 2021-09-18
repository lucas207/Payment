using Eice.Payment.API.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eice.Payment.API.Command
{
    public class ClientGetAllCommand : Command, IRequest<IEnumerable<ClientDto>>
    {
        public override bool IsValid()
        {
            return true;
        }
    }
}
