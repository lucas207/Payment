using Eice.Payment.Domain.Partner;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eice.Payment.Infra.Partner
{
    public class PartnerQueryRepository : IPartnerQueryRepository
    {
        private readonly IMongoCollection<PartnerEntity> _partners;
        public PartnerQueryRepository(IMongoClient client)
        {
            var _database = client.GetDatabase("EicePagamentosDB");
            _partners = _database.GetCollection<PartnerEntity>("Partner");
        }

        public Task<PartnerEntity> Get(ObjectId Id)
        {
            var filter = Builders<PartnerEntity>.Filter.Eq(c => c.Id, Id);
            var client = _partners.Find(filter).FirstOrDefaultAsync();

            return client;
        }

        public async Task<IEnumerable<PartnerEntity>> GetAll()
        {
            var all = await _partners.Find(_ => true).ToListAsync();

            return all;
        }
    }
}
