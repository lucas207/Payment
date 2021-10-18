using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Eice.Payment.Domain.Lancamento
{
    public class LancamentoEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
    }
}
