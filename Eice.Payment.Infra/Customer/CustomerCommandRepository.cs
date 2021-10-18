using Eice.Payment.Domain.Customer;
using Eice.Payment.Domain.Lancamento;
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
                .Set(c => c.Lancamento, entity.Lancamento)
                .Set(c => c.SaldoAtual, entity.SaldoAtual);
            var result = await _collection.UpdateOneAsync(filter, update);

            return result.ModifiedCount == 1;
        }

        public async Task<bool> Delete(ObjectId Id)
        {
            var filter = Builders<CustomerEntity>.Filter.Eq(c => c.Id, Id);
            var result = await _collection.DeleteOneAsync(filter);

            return result.DeletedCount == 1;
        }

        public async Task<bool> InsertLancamento(CustomerEntity customerEntity, LancamentoEntity lancamentoEntity)
        {
            var filter = Builders<CustomerEntity>.Filter.Eq(c => c.Id, customerEntity.Id);
            var update = Builders<CustomerEntity>.Update
                .Push(a => a.Lancamento, lancamentoEntity);

            var result = await _collection.UpdateOneAsync(filter, update);
            return result.ModifiedCount == 1;
            //& Builders<CustomerEntity>.Filter.ElemMatch(x => x.Lancamento, Builders<LancamentoEntity>.Filter.Eq(a => a.Id, entity.Id);
        }
    }
}
