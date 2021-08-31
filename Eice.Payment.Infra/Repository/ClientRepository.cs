using Eice.Payment.Domain.Client;
using Eice.Payment.Domain.Interface.Repository;
using System;
using System.Collections.Generic;

namespace Eice.Payment.Infra.Repository
{
    public class ClientRepository : Repository<ClientModel>, IClienteRepository
    {
        private readonly MongoDbContext dbContext;

        public ClientRepository()
        {
            dbContext = new MongoDbContext();
        }

        public bool Delete(ClientModel entity)
        {
            throw new NotImplementedException();
        }

        public ClientModel Get(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ClientModel> GetAll(int Id)
        {
            throw new NotImplementedException();
        }

        public bool Save(ClientModel entity)
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedAt = DateTime.Now;

            dbContext.Clientes.InsertOne(entity);
            return true;
        }

        public bool Update(ClientModel entity)
        {
            throw new NotImplementedException();
        }

    }
}
