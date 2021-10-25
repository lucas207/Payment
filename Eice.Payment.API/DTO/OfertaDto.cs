using Eice.Payment.Domain.Oferta;
using System;

namespace Eice.Payment.API.DTO
{
    public class OfertaDto
    {
        public string Id { get; set; }
        public string CoinIdOffer { get; set; }
        public string CoinIdReceive { get; set; }
        public int QuantityOffer { get; set; }
        public int QuantityReceive { get; set; }
        public string Status { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
