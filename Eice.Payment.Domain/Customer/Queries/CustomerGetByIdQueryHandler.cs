using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Eice.Payment.Domain.Customer.Queries
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
