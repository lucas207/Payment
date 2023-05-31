using System;

namespace Eice.Payment.Domain.Oferta.Queries
{
    public class OfertaDto
    {
        public string Id { get; set; }
        public string CoinIdOffer { get; set; }
        public string CoinIdReceive { get; set; }
        public decimal QuantityOffer { get; set; }
        public decimal QuantityReceive { get; set; }
        public EStatusOferta Status { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
