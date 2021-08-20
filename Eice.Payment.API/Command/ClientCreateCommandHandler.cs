using Eice.Payment.API.Notification;
using Eice.Payment.Infra;
using Eice.Payment.Infra.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Eice.Payment.API.Command
{
    public class ClientCreateCommandHandler : CommandHandler, IRequestHandler<ClientCreateCommand, Guid>
    {
        public ClientCreateCommandHandler(IMediator bus) : base(bus)
        {
        }

        public async Task<Guid> Handle(ClientCreateCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) { GetNotificationsErrors(request); return default; }

            try
            {
                //inserir context no conteiner
                MongoDbContext dbContext = new MongoDbContext();

                //metodo to map
                ClientModel entity = new ClientModel();
                entity.Id = Guid.NewGuid();
                entity.Name = request.Name;
                entity.TipoPessoa = request.TipoPessoa;
                entity.CpfCnpj = request.CpfCnpj;
                
                //automatic field
                entity.CreatedAt = DateTime.Now;

                dbContext.Clientes.InsertOne(entity);

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
