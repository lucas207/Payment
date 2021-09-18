using Eice.Payment.Domain.Customer;
using System;

namespace Eice.Payment.API.DTO
{
    public class CustomerDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CpfCnpj { get; set; }
        public ETipoPessoa TipoPessoa { get; set; }

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
