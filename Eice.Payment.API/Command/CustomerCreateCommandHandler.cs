using Eice.Payment.API.Notification;
using Eice.Payment.Domain.Customer;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Eice.Payment.API.Command
{
    public class CustomerCreateCommandHandler : CommandHandler, IRequestHandler<CustomerCreateCommand, string>
    {
        private readonly ICustomerRepository _clienteRepository;
        public CustomerCreateCommandHandler(IMediator bus, ICustomerRepository clienteRepository) : base(bus)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<string> Handle(CustomerCreateCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) { GetNotificationsErrors(request); return default; }

            try
            {
                
                //metodo to map
                Customer entity = new Customer();
                entity.Name = request.Name;
                entity.TipoPessoa = request.TipoPessoa;
                entity.CpfCnpj = request.CpfCnpj;
                await _clienteRepository.Create(entity);

                return entity.Id.ToString();

            }
            catch (Exception ex)
            {
                await _bus.Publish(new ExceptionNotification("500", ex.Message, null, ex.StackTrace));  
                return default;
            }
        }
    }
}
