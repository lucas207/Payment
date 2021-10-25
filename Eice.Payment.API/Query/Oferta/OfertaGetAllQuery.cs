using Eice.Payment.API.DTO;
using MediatR;
using System.Collections.Generic;

namespace Eice.Payment.API.Query.Oferta
{
    public class OfertaGetAllQuery : Query, IRequest<IEnumerable<OfertaDto>>
    {
    }
}
