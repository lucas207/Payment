using Eice.Payment.Domain.Customer;
using Eice.Payment.Domain.Customer.Commands;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Eice.Payment.Infra.Customer
{
    public class CustomerCommandRepository : ICustomerCommandRepository
    {
        private readonly IMongoCollection<CustomerEntity> _collection;

        public CustomerCommandRepository(IMongoClient client, IConfiguration configuration)
        {
            var _databaseName = configuration.GetSection("MongoConnection:Database").Value;
            var _database = client.GetDatabase(_databaseName);
            _collection = _database.GetCollection<CustomerEntity>("Customer");
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
                .Set(c => c.Name, entity.Name)
                .Set(c => c.Lancamentos, entity.Lancamentos)
                .Set(c => c.SaldoAtual, entity.SaldoAtual);
            var result = await _collection.UpdateOneAsync(filter, update);

            return result.ModifiedCount == 1;
        }

        public async Task<bool> Delete(ObjectId Id)
        {
            //var filter = Builders<CustomerEntity>.Filter.Eq(c => c.Id, Id);
            //var result = await _collection.DeleteOneAsync(filter);
            var result = await _collection.DeleteOneAsync(x => x.Id == Id);

            return result.DeletedCount == 1;
        }

    }
}
