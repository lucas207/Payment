using MediatR;
using System.Collections.Generic;

namespace Eice.Payment.Domain.Customer.Queries
{
    public class CustomerGetAllQuery : Query, IRequest<IEnumerable<CustomerDto>>
    {
        public string PartnerId { get; set; }
    }
}
