using Payment.Domain.Default;

namespace Payment.Domain.Client
{
    public class ClientPfDTO : Dto
    {
        public string Name { get; set; }
        public int Cpf { get; set; }

        public string CpfFormated
        {
            get { return Cpf.ToString(@"000\.000\.000\-00"); }
        }
    }
}
