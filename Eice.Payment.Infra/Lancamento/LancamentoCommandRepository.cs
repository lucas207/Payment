using Eice.Payment.Domain.Lancamento;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Eice.Payment.Infra.Lancamento
{
    public class LancamentoCommandRepository : ILancamentoCommandRepository
    {
        private readonly IMongoCollection<LancamentoEntity> _collection;

        public LancamentoCommandRepository(IMongoClient client)
        {
            var _database = client.GetDatabase("EicePagamentosDB");
            _collection = _database.GetCollection<LancamentoEntity>("Lancamento");
        }

        public async Task<ObjectId> Create(LancamentoEntity entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity.Id;
        }

        public Task<bool> Delete(ObjectId Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(ObjectId Id, LancamentoEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
