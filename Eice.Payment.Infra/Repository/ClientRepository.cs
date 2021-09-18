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
            var filter = Builders<Client>.Filter.Eq(c => c.Id, Id);
            var client = _clientes.Find(filter).FirstOrDefaultAsync();

            return client;
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            var clientes = await _clientes.Find(_ => true).ToListAsync();

            return clientes;
        }

        public async Task<bool> Update(ObjectId Id, Client entity)
        {
            var filter = Builders<Client>.Filter.Eq(c => c.Id, Id);
            var update = Builders<Client>.Update
                .Set(c => c.Name, entity.Name)
                .Set(c => c.TipoPessoa, entity.TipoPessoa)
                .Set(c => c.CpfCnpj, entity.CpfCnpj);
            var result = await _clientes.UpdateOneAsync(filter, update);

            return result.ModifiedCount == 1;
        }

        public async Task<bool> Delete(ObjectId Id)
        {
            var filter = Builders<Client>.Filter.Eq(c => c.Id, Id);
            var result = await _clientes.DeleteOneAsync(filter);

            return result.DeletedCount == 1;
        }
    }
}
