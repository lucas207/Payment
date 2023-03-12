using System.ComponentModel.DataAnnotations;

namespace Eice.Payment.Domain.Partner
{
    public class PartnerEntity : Entity
    {
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public string CoinName { get; set; }
        public string NameAlias { get; set; }
        public string Image { get; set; }
        [Required]
        public string AuthenticationKey { get; set; }
        public bool EnableExchanges { get; set; }
    }
}
