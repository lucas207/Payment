using Payment.Domain.Client;
using Payment.Domain.Default;
using Payment.Domain.Parcela;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Domain.Pagamento
{
    public class PagamentoDTO : Dto
    {
        public ClientPfDTO Client { get; set; }
        public Decimal TotalValue { get; set; }
        public Decimal EntryValue { get; set; }
        //public DateTime ExpirationDate { get; }
        public IList<ParcelaDTO> Parcelas { get; set; }
    }
}
