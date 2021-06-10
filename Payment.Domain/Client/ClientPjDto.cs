using Payment.Domain.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Domain.Client
{
    public class ClientPjDto : Dto
    {
        public string Name { get; set; }
        public int Cnpj { get; set; }

        public string CpfFormated
        {
            get { return Cnpj.ToString(@"00\.000\.000\/0000\-00"); }
        }
    }
}
