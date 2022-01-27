using MediatR;
using System.Collections.Generic;

namespace Eice.Payment.Domain.Partner.Queries
{
    public class PartnerGetAllQuery : Query, IRequest<IEnumerable<PartnerDto>>
    {
    }
}
