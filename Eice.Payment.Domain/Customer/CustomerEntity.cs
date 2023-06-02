using Eice.Payment.Domain.Lancamento;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eice.Payment.Domain.Customer
{
    public class CustomerEntity : Entity
    {
        [Required]
        public string PartnerId { get; set; }
        [Required]
        public string Cpf { get; set; }
        public string Name { get; set; }

        public decimal SaldoAtual { get; set; }
        public List<LancamentoEntity> Lancamentos { get; set; } = new();
        //bool possui moedas em outras instituições
        //bool permirtir negociar moedas desse partner (blackList)
    }

}
