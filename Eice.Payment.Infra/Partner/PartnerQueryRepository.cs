using Eice.Payment.Domain.Partner;
using Eice.Payment.Domain.Partner.Queries;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eice.Payment.Infra.Partner
{
    public class PartnerQueryRepository : IPartnerQueryRepository
    {
        private readonly IMongoCollection<PartnerEntity> _collection;
        public PartnerQueryRepository(IMongoClient client, IConfiguration configuration)
        {
            var _databaseName = configuration.GetSection("MongoConnection:Database").Value;
            var _database = client.GetDatabase(_databaseName);
            _collection = _database.GetCollection<PartnerEntity>("Partner");
        }

        public Task<PartnerEntity> Get(ObjectId Id)
        {
            var filter = Builders<PartnerEntity>.Filter.Eq(c => c.Id, Id);
            var client = _collection.Find(filter).FirstOrDefaultAsync();

            return client;
        }

        public Task<PartnerEntity> Get(string Id)
        {
            var filter = Builders<PartnerEntity>.Filter.Eq(c => c.Id, new ObjectId(Id));
            var client = _collection.Find(filter).FirstOrDefaultAsync();
            return client;
        }

        public Task<PartnerEntity> GetByAuthenticationKey(string authenticationKey)
        {
            var filter = Builders<PartnerEntity>.Filter.Eq(c => c.AuthenticationKey, authenticationKey);
            var client = _collection.Find(filter).FirstOrDefaultAsync();
            return client;
        }

        public async Task<IEnumerable<PartnerEntity>> GetAll()
        {
            var all = await _collection.Find(_ => true).ToListAsync();
            return all;
        }

        public async Task<IEnumerable<PartnerEntity>> GetAllEnableExchange()
        {
            var all = await _collection.Find(x => x.EnableExchanges).ToListAsync();
            return all;
        }
    }
}
