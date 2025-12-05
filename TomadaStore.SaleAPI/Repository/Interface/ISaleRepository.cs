using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.SaleAPI.Model;

namespace TomadaStore.SaleAPI.Repository.Interface
{
    public interface ISaleRepository
    {
        Task CreateSaleAsync(CustomerResponseDTO customer,
                            ProductResponseDTO product,
                            SaleRequestDTO sale);
    }
}
