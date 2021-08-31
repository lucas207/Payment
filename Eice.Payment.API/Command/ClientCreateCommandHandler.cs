using Eice.Payment.API.Notification;
using Eice.Payment.Domain.Client;
using Eice.Payment.Infra;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Eice.Payment.API.Command
{
    public class ClientCreateCommandHandler : CommandHandler, IRequestHandler<ClientCreateCommand, Guid>
    {
        private readonly IClienteRepository _clienteRepository;
        public ClientCreateCommandHandler(IMediator bus, IClienteRepository clienteRepository) : base(bus)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<Guid> Handle(ClientCreateCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) { GetNotificationsErrors(request); return default; }

            try
            {
                
                //metodo to map
                ClientModel entity = new ClientModel();
                entity.Name = request.Name;
                entity.TipoPessoa = request.TipoPessoa;
                entity.CpfCnpj = request.CpfCnpj;
                _clienteRepository.Save(entity);

                return entity.Id;

            }
            catch (Exception ex)
            {
                await _bus.Publish(new ExceptionNotification("500", ex.Message, null, ex.StackTrace));  
                return default;
            }
        }
    }
}
