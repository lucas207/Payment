using Eice.Payment.API.DTO;
using Eice.Payment.Domain.Customer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Eice.Payment.API.Query.Customer
{
    public class CustomerGetByIdQueryHandler : QueryHandler<CustomerEntity>, IRequestHandler<CustomerGetByIdQuery, CustomerDto>
    {
        public CustomerGetByIdQueryHandler(IMediator bus, ICustomerQueryRepository customerRepository) : base(bus, customerRepository)
        {
        }

        public async Task<CustomerDto> Handle(CustomerGetByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
