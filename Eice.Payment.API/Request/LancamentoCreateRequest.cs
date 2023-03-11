namespace Eice.Payment.API.Request
{
    public class LancamentoCreateRequest
    {
        public string CustomerId { get; set; }
        public decimal Quantity { get; set; }
        public string Description { get; set; }
    }
}
