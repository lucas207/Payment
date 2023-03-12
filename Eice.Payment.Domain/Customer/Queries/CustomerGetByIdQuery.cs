using MediatR;

namespace Eice.Payment.Domain.Customer.Queries
{
    public class CustomerGetByIdQuery : Query, IRequest<CustomerDetailDto>
    {
        public string Id { get; set; }
    }
}
