using System.ComponentModel.DataAnnotations;

namespace Eice.Payment.Domain.Lancamento
{
    public class LancamentoEntity : Entity
    {
        [Required]
        public decimal Quantity { get; set; }
        public string Description { get; set; }
    }
}
