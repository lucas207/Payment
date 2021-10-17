using Eice.Payment.API.DTO;
using MediatR;
using System.Collections.Generic;

namespace Eice.Payment.API.Query.Partner
{
    public class PartnerGetAllQuery : Query, IRequest<IEnumerable<PartnerDto>>
    {
    }
}
