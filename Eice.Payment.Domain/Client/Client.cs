using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Eice.Payment.Domain.Client
{
    public class Client
    {
        public ObjectId Id { get; }
        [Required]
        public string Name { get; set; }
        public string CpfCnpj { get; set; }
        public ETipoPessoa TipoPessoa { get; set; }

    }

    public enum ETipoPessoa
    {
        Fisica,
        Juridica
    }
}
