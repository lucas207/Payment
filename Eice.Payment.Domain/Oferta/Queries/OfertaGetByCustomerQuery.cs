using MediatR;
using System.Collections.Generic;

namespace Eice.Payment.Domain.Oferta.Queries
{
    public class OfertaGetByCustomerQuery : Query, IRequest<IEnumerable<OfertaDto>>
    {
        public string CustomerId { get; set; }
    }
}