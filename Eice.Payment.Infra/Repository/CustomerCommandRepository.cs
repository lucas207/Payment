using Eice.Payment.Domain.Customer;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Eice.Payment.Infra.Repository
{
    public class CustomerCommandRepository : ICustomerCommandRepository
    {
        private readonly IMongoCollection<Customer> _clientes;

        public CustomerCommandRepository(IMongoClient client)
        {
            var _database = client.GetDatabase("EicePagamentosDB");
            _clientes = _database.GetCollection<Customer>("Client");
        }

        public async Task<ObjectId> Create(Customer entity)
        {
            await _clientes.InsertOneAsync(entity);
            return entity.Id;
        }

        public async Task<bool> Update(ObjectId Id, Customer entity)
        {
            var filter = Builders<Customer>.Filter.Eq(c => c.Id, Id);
            var update = Builders<Customer>.Update
                .Set(c => c.Name, entity.Name)
                .Set(c => c.TipoPessoa, entity.TipoPessoa)
                .Set(c => c.CpfCnpj, entity.CpfCnpj);
            var result = await _clientes.UpdateOneAsync(filter, update);

            return result.ModifiedCount == 1;
        }

        public async Task<bool> Delete(ObjectId Id)
        {
            var filter = Builders<Customer>.Filter.Eq(c => c.Id, Id);
            var result = await _clientes.DeleteOneAsync(filter);

            return result.DeletedCount == 1;
        }
    }
}
