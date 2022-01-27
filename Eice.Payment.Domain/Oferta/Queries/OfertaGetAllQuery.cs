using MediatR;
using System.Collections.Generic;

namespace Eice.Payment.Domain.Oferta.Queries
{
    public class OfertaGetAllQuery : Query, IRequest<IEnumerable<OfertaDto>>
    {
    }
}
