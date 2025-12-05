using TomadaStore.Models.DTOs.Product;

namespace TomadaStore.ProductAPI.Service.Interface
{
    public interface IProductService
    {
        Task<List<ProductResponseDTO>> GetAllProductsAsync();
        Task<ProductResponseDTO> GetProductByIdAsync(string id);
        Task CreateProductAsync(ProductRequestDTO productDto);
        //Task UpdateProductAsync(string id, ProductRequestDTO productDto);
        //Task DeleteProductAsync(string id);
    }
}
