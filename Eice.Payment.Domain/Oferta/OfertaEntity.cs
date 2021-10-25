using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Eice.Payment.Domain.Oferta
{
    public class OfertaEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string CustomerIdCreated { get; set; }
        public string CoinIdReceive { get; set; }
        public int QuantityOffer { get; set; }
        public int QuantityReceive { get; set; }
        public EStatusOffer Status { get; set; }
    }

    public enum EStatusOffer
    {
        Open,
        Executed,
        Error
    }
}
