using Eice.Payment.Domain.Oferta;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eice.Payment.Infra.Oferta
{
    public class OfertaQueryRepository : IOfertaQueryRepository
    {
        private readonly IMongoCollection<OfertaEntity> _collection;

        public OfertaQueryRepository(IMongoClient client)
        {
            var _database = client.GetDatabase("EicePagamentosDB");
            _collection = _database.GetCollection<OfertaEntity>("Oferta");
        }

        public Task<OfertaEntity> Get(ObjectId Id)
        {
            throw new NotImplementedException();
        }

        public Task<OfertaEntity> Get(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OfertaEntity>> GetAll()
        {
            var ofertas = await _collection.Find(_ => true).ToListAsync();
            return ofertas;
        }
    }
}
