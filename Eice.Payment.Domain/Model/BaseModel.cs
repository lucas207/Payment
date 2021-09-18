using MongoDB.Bson;
using System;

namespace Eice.Payment.Domain.Model
{
    public class BaseModel
    {
        public ObjectId Id { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
    }
}
