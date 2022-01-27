using Eice.Payment.Domain.Oferta;
using Eice.Payment.Domain.Oferta.Commands;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Eice.Payment.Infra.Oferta
{
    public class OfertaCommandRepository : IOfertaCommandRepository
    {
        private readonly IMongoCollection<OfertaEntity> _collection;

        public OfertaCommandRepository(IMongoClient client)
        {
            var _database = client.GetDatabase("EicePagamentosDB");
            _collection = _database.GetCollection<OfertaEntity>("Oferta");
        }

        public async Task<ObjectId> Create(OfertaEntity entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity.Id;
        }

        public Task<bool> Delete(ObjectId Id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(ObjectId Id, OfertaEntity entity)
        {
            var filter = Builders<OfertaEntity>.Filter.Eq(c => c.Id, Id);
            var update = Builders<OfertaEntity>.Update
                .Set(c => c.CustomerAccepted, entity.CustomerAccepted)
                .Set(c => c.Status, entity.Status);
                //.Set(c => c.SaldoAtual, entity.SaldoAtual);
            var result = await _collection.UpdateOneAsync(filter, update);

            return result.ModifiedCount == 1;
        }
    }
}
