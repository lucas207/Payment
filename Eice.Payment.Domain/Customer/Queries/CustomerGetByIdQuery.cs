using MediatR;
using System;

namespace Eice.Payment.Domain.Customer.Queries
{
    public class CustomerGetByIdQuery : IRequest<CustomerDto>
    {
        public string Id { get; set; }

        //public override bool IsValid()
        //{
        //    return true;
        //}
    }
}
