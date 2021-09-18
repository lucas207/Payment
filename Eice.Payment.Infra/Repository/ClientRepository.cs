using Eice.Payment.Domain.Client;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eice.Payment.Infra.Repository
{
    public class ClientRepository : IClienteRepository
    {
        private readonly IMongoCollection<Client> _clientes;

        public ClientRepository(IMongoClient client)
        {
            var _database = client.GetDatabase("EicePagamentosDB");
            _clientes = _database.GetCollection<Client>("Client");
        }

        public async Task<ObjectId> Create(Client entity)
        {
            await _clientes.InsertOneAsync(entity);
            return entity.Id;
        }

        public Task<Client> Get(ObjectId Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            var clientes = await _clientes.Find(_ => true).ToListAsync();

            return clientes;
        }

        public Task<bool> Update(ObjectId Id, Client entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(ObjectId Id)
        {
            throw new NotImplementedException();
        }
    }
}
