using Eice.Payment.Domain.Notification;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Eice.Payment.Domain.Customer.Commands
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
                    Cpf = request.Cpf,
                    Name = request.Name
                };

                await _commandRepository.Create(entity);

                return entity.Id.ToString();

            }
            catch (Exception ex)
            {
                await _bus.Publish(new ExceptionNotification("010", ex.Message, null), cancellationToken);
                return default;
            }
        }
    }
}
