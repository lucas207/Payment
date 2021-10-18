using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eice.Payment.API.DTO
{
    public class LancamentoDto
    {
        public int Saldo { get; set; }
        public IEnumerable<LancamentoItemDto> LancamentoItems { get; set; }
    }

    public class LancamentoItemDto
    {
        public int Quantidade { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
