using Eice.Payment.Domain.Customer;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eice.Payment.Infra.Customer
{
    public class CustomerQueryRepository : ICustomerQueryRepository
    {
        private readonly IMongoCollection<CustomerEntity> _clientes;

        public CustomerQueryRepository(IMongoClient client)
        {
            var _database = client.GetDatabase("EicePagamentosDB");
            _clientes = _database.GetCollection<CustomerEntity>("Client");
        }

        public Task<CustomerEntity> Get(ObjectId Id)
        {
            var filter = Builders<CustomerEntity>.Filter.Eq(c => c.Id, Id);
            var client = _clientes.Find(filter).FirstOrDefaultAsync();

            return client;
        }

        public async Task<IEnumerable<CustomerEntity>> GetAll()
        {
            var clientes = await _clientes.Find(_ => true).ToListAsync();

            return clientes;
        }

    }
}
