using System;

namespace Eice.Payment.Domain.Lancamento.Queries
{
    public class LancamentoDto
    {
        public string Id { get; set; }
        public decimal Quantidade { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
