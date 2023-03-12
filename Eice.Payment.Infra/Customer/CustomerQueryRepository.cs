using Eice.Payment.Domain.Customer;
using Eice.Payment.Domain.Customer.Queries;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eice.Payment.Infra.Customer
{
    public class CustomerQueryRepository : ICustomerQueryRepository
    {
        private readonly IMongoCollection<CustomerEntity> _collection;

        public CustomerQueryRepository(IMongoClient client, IConfiguration configuration)
        {
            var _databaseName = configuration.GetSection("MongoConnection:Database").Value;
            var _database = client.GetDatabase(_databaseName);
            _collection = _database.GetCollection<CustomerEntity>("Customer");
        }

        public Task<CustomerEntity> Get(ObjectId Id)
        {
            var filter = Builders<CustomerEntity>.Filter.Eq(c => c.Id, Id);
            var client = _collection.Find(filter).FirstOrDefaultAsync();
            return client;
        }

        public Task<CustomerEntity> Get(string Id)
        {
            var filter = Builders<CustomerEntity>.Filter.Eq(c => c.Id, new ObjectId(Id));
            var client = _collection.Find(filter).FirstOrDefaultAsync();
            return client;
        }

        public async Task<IEnumerable<CustomerEntity>> GetAll()
        {
            var clientes = await _collection.Find(_ => true).ToListAsync();
            return clientes;
        }

        public IEnumerable<CustomerEntity> GetAllByPartnerId(string partnerId)
        {
            var filter = Builders<CustomerEntity>.Filter.Eq(c => c.PartnerId, partnerId);
            var clientes = _collection.Find(filter).ToEnumerable();
            return clientes;
        }

        public IEnumerable<CustomerEntity> GetAllByCpf(string cpf)
        {
            var filter = Builders<CustomerEntity>.Filter.Eq(c => c.Cpf, cpf);
            var clientes = _collection.Find(filter).ToEnumerable();
            return clientes;
        }
    }
}
