using Eice.Payment.Domain.Customer;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eice.Payment.Infra.Repository
{
    public class CustomerQueryRepository : ICustomerQueryRepository
    {
        private readonly IMongoCollection<Customer> _clientes;

        public CustomerQueryRepository(IMongoClient client)
        {
            var _database = client.GetDatabase("EicePagamentosDB");
            _clientes = _database.GetCollection<Customer>("Client");
        }

        public Task<Customer> Get(ObjectId Id)
        {
            var filter = Builders<Customer>.Filter.Eq(c => c.Id, Id);
            var client = _clientes.Find(filter).FirstOrDefaultAsync();

            return client;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            var clientes = await _clientes.Find(_ => true).ToListAsync();

            return clientes;
        }

    }
}
