using Eice.Payment.Domain.Oferta;
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

        public Task<bool> Update(ObjectId Id, OfertaEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
