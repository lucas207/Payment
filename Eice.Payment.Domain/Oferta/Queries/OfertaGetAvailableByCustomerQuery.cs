using MediatR;
using System.Collections.Generic;

namespace Eice.Payment.Domain.Oferta.Queries
{
    public class OfertaGetAvailableByCustomerQuery : Query, IRequest<IEnumerable<OfertaDto>>
    {
        public string PartnerId { get; set; }
        public string CustomerId { get; set; }
    }
}
