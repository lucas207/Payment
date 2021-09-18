using Eice.Payment.Domain.Client;
using System;

namespace Eice.Payment.API.DTO
{
    public class ClientDto : Client
    {
        //public string Name { get; set; }
        //public int CpfCnpj { get; set; }
        //public ETipoPessoa TipoPessoa { get; set; }

        public string CpfCnpjFormated
        {
            get
            {
                if (TipoPessoa == ETipoPessoa.Fisica)
                    return Convert.ToUInt64(CpfCnpj).ToString(@"000\.000\.000\-00");
                else
                    return Convert.ToUInt64(CpfCnpj).ToString(@"00\.000\.000\/0000\-00");
            }
        }
    }
}
