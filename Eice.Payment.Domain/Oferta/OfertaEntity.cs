using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Eice.Payment.Domain.Oferta
{
    public class OfertaEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public CustomerOferta CustomerCreated { get; set; }
        public CustomerOferta CustomerAccepted { get; set; }
        public CoinOferta CoinOffer { get; set; }
        public CoinOferta CoinReceive { get; set; }
        public decimal QuantityOffer { get; set; }
        public decimal QuantityReceive { get; set; }
        public EStatusOferta Status { get; set; }
    }

    public class CustomerOferta
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
    }

    public class CoinOferta
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
    }

    public enum EStatusOferta
    {
        Open,
        Executed,
        Error
    }

}
