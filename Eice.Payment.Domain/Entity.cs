using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Eice.Payment.Domain
{
    public class Entity
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
