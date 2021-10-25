using Eice.Payment.API.Notification;
using Eice.Payment.Domain.Customer;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Eice.Payment.API.Command.Customer
{
    public class CustomerCreateCommandHandler : CommandHandler<CustomerEntity>, IRequestHandler<CustomerCreateCommand, string>
    {
        public CustomerCreateCommandHandler(IMediator bus, ICustomerCommandRepository customerRepository)
            : base(bus, customerRepository)
        {
        }

        public async Task<string> Handle(CustomerCreateCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) { GetNotificationsErrors(request); return default; }

            try
            {
                //Validar request.PartnerId existe
                //Validar request.AuthenticationKey pertence ao PartnerId

                //metodo to map
                CustomerEntity entity = new()
                {
                    PartnerId = request.PartnerId,
                    Cpf = request.Cpf
                };

                await _commandRepository.Create(entity);

                return entity.Id.ToString();

            }
            catch (Exception ex)
            {
                await _bus.Publish(new ExceptionNotification("500", ex.Message, null, ex.StackTrace), cancellationToken);
                return default;
            }
        }
    }
}
