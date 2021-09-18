using Eice.Payment.API.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eice.Payment.API.Query
{
    public class GetClientByIdQuery : IRequest<CustomerDto>
    {
        public Guid Id { get; set; }
    }
}
