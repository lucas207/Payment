using MediatR;
using Payment.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.API.Commands
{
    public class PaymentCreateCommand : IRequest<PaymentModel>
    {
        public string Name { get; set; }
    }
}
