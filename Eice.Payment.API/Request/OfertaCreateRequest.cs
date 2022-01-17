namespace Eice.Payment.API.Request
{
    public class OfertaCreateRequest
    {
        public string CustomerIdCreated { get; set; }
        public decimal QuantityOffer { get; set; }
        public decimal QuantityReceive { get; set; }
        public string CoinIdReceive { get; set; }
    }
}
