using System;
using System.Collections.Generic;

namespace Eice.Payment.API.DTO
{
    public class LancamentoDto
    {
        public decimal Saldo { get; set; }
        public List<LancamentoItemDto> LancamentoItems { get; set; } = new List<LancamentoItemDto>();
    }

    public class LancamentoItemDto
    {
        public decimal Quantidade { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
