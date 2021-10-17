using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Eice.Payment.Domain.Customer
{
    public class CustomerEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [Required]
        public string PartnerId { get; set; }
        [Required]
        public string Cpf { get; set; }

    }

}
