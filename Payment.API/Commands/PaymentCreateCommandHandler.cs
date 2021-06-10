using MediatR;
using Payment.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.API.Commands
{
    public class PaymentCreateCommandHandler : IRequestHandler<PaymentCreateCommand, PaymentModel>
    {
        public Task<PaymentModel> Handle(PaymentCreateCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
