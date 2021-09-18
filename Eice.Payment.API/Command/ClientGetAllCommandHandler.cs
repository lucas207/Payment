using Eice.Payment.API.DTO;
using Eice.Payment.API.Notification;
using Eice.Payment.Domain.Client;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Eice.Payment.API.Command
{
    public class ClientGetAllCommandHandler : CommandHandler, IRequestHandler<ClientGetAllCommand, IEnumerable<ClientDto>>
    {
        private readonly IClienteRepository _clienteRepository;
        public ClientGetAllCommandHandler(IMediator bus, IClienteRepository clienteRepository) : base(bus)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<ClientDto>> Handle(ClientGetAllCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) { GetNotificationsErrors(request); return default; }

            try
            {
                var list = await _clienteRepository.GetAll();

                List<ClientDto> resp = new List<ClientDto>();
                foreach (var item in list)
                {
                    resp.Add(new ClientDto { Id = item.Id, CpfCnpj = item.CpfCnpj, Name = item.Name, TipoPessoa = item.TipoPessoa });
                }

                return resp;
            }
            catch (Exception ex)
            {
                await _bus.Publish(new ExceptionNotification("500", ex.Message, null, ex.StackTrace));
                return default;
            }
        }
    }
}
