using Eice.Payment.API.Notification;
using Eice.Payment.Domain.Client;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Eice.Payment.API.Command
{
    public class ClientCreateCommandHandler : CommandHandler, IRequestHandler<ClientCreateCommand, string>
    {
        private readonly IClienteRepository _clienteRepository;
        public ClientCreateCommandHandler(IMediator bus, IClienteRepository clienteRepository) : base(bus)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<string> Handle(ClientCreateCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) { GetNotificationsErrors(request); return default; }

            try
            {
                
                //metodo to map
                Client entity = new Client();
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
