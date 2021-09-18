using Eice.Payment.API.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eice.Payment.API.Command
{
    public class CustomerGetAllCommand : Command, IRequest<IEnumerable<CustomerDto>>
    {
        public override bool IsValid()
        {
            return true;
        }
    }
}
