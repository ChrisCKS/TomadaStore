namespace TomadaStore.Models.DTOs.Sale
{
    public class SaleResponseDTO
    {
        public string Id { get; init; }
        public int CustomerId { get; init; }
        public List<string> ProductIds { get; init; }
        public DateTime SaleDate { get; init; }
        public decimal TotalPrice { get; init; }
    }
}
