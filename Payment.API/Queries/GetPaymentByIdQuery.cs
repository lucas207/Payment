using MediatR;
using Payment.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.API.Queries
{
    public class GetPaymentByIdQuery : IRequest<PaymentModel>
    {
        public int Id { get; set; }
    }
}
