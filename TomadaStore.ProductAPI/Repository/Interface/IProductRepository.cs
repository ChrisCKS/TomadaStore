using TomadaStore.Models.DTOs.Product;

namespace TomadaStore.ProductAPI.Repository.Interface
{
    public interface IProductRepository
    {
        Task<List<ProductResponseDTO>> GetAllProductsAsync();
        Task<ProductResponseDTO> GetProductByIdAsync(string id);
        Task CreateProductAsync(ProductRequestDTO productDto);
        //Task UpdateProductAsync(string id, ProductRequestDTO productDto);
        //Task DeleteProductAsync(string id);
    }
}
