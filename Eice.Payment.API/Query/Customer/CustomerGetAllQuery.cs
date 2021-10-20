using Eice.Payment.API.DTO;
using MediatR;
using System.Collections.Generic;

namespace Eice.Payment.API.Query.Customer
{
    public class CustomerGetAllQuery : Query, IRequest<IEnumerable<CustomerDto>>
    {
    }
}
