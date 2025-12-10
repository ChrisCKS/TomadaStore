namespace TomadaStore.Models.DTOs.Payment
{
    public class PaymentRequestDTO
    {
        public string SaleId { get; init; }
        public decimal Amount { get; init; }
        public string Status { get; init; }
    }
}
