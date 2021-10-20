using System;

namespace Eice.Payment.API.DTO
{
    public class PartnerDto
    {
        //public string Id { get; set; }
        public string Name { get; set; }
        public string Cnpj { get; set; }
        //public DateTime CreationTime { get; set; }
        //Alias
        //Image

        public string CnpjFormated
        {
            get
            {
                return Convert.ToUInt64(Cnpj).ToString(@"00\.000\.000\/0000\-00");
            }
        }
    }
}
