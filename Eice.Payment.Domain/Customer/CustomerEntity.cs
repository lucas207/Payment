using Eice.Payment.Domain.Lancamento;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
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

        public int SaldoAtual { get; set; }
        public List<LancamentoEntity> Lancamentos { get; set; } = new();
        //bool possui moedas em outras instituições
        //bool permirtir negociar moedas desse partner
    }

}
