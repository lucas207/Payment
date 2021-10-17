using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Eice.Payment.Domain.Partner
{
    public class PartnerEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public string AuthenticationKey { get; set; }
    }
}
