using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eice.Payment.Domain.Oferta.Queries
{
    public class OfertaGetAvailableByCustomerQuery : Query, IRequest<IEnumerable<OfertaDto>>
    {
        public string CustomerId { get; set; }
    }
}
