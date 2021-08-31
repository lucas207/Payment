using Eice.Payment.Domain.Client;

namespace Eice.Payment.API.DTO
{
    public class ClientDto : ClientModel
    {
        //public string Name { get; set; }
        //public int CpfCnpj { get; set; }
        //public ETipoPessoa TipoPessoa { get; set; }

        public string CpfCnpjFormated
        {
            get
            {
                if (TipoPessoa == ETipoPessoa.Fisica)
                    return CpfCnpj.ToString(@"000\.000\.000\-00");
                else
                    return CpfCnpj.ToString(@"00\.000\.000\/0000\-00");
            }
        }
    }
}
