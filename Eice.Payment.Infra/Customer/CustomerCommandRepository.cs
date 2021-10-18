using Eice.Payment.Domain;
using Eice.Payment.Domain.Customer;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Eice.Payment.Infra.Customer
{
    public class CustomerCommandRepository : ICustomerCommandRepository
    {
        private readonly IMongoCollection<CustomerEntity> _collection;

        public CustomerCommandRepository(IMongoClient client)
        {
            var _database = client.GetDatabase("EicePagamentosDB");
            _collection = _database.GetCollection<CustomerEntity>("Client");
        }

        public async Task<ObjectId> Create(CustomerEntity entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity.Id;
        }

        public async Task<bool> Update(ObjectId Id, CustomerEntity entity)
        {
            var filter = Builders<CustomerEntity>.Filter.Eq(c => c.Id, Id);
            var update = Builders<CustomerEntity>.Update
                .Set(c => c.PartnerId, entity.PartnerId)
                //.Set(c => c.TipoPessoa, entity.TipoPessoa)
                .Set(c => c.Cpf, entity.Cpf);
            var result = await _collection.UpdateOneAsync(filter, update);

            return result.ModifiedCount == 1;
        }

        public async Task<bool> Delete(ObjectId Id)
        {
            var filter = Builders<CustomerEntity>.Filter.Eq(c => c.Id, Id);
            var result = await _collection.DeleteOneAsync(filter);

            return result.DeletedCount == 1;
        }
    }
}
