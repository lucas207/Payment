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
    public class ClientCreateCommandHandler : IRequestHandler<ClientCreateCommand, Guid>
    {
        public Task<Guid> Handle(ClientCreateCommand request, CancellationToken cancellationToken)
        {
            //Passar o Fluent Validation

            MongoDbContext dbContext = new MongoDbContext();

            ClientModel entity = new ClientModel();
            entity.Id = Guid.NewGuid();
            dbContext.Clientes.InsertOne(entity);

            return Task.FromResult(entity.Id);
        }
    }
}
