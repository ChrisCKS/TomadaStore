using MongoDB.Bson;
using MongoDB.Driver;
using TomadaStore.Models.DTOs.Category;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.Models;
using TomadaStore.ProductAPI.Data;
using TomadaStore.ProductAPI.Repository.Interface;

namespace TomadaStore.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ILogger<ProductRepository> _logger;
        private readonly IMongoCollection<Product> _mongoCollection;
        private readonly ConnectionDB _connection;

        public ProductRepository(ILogger<ProductRepository> logger, ConnectionDB connection)
        {
            _logger = logger;
            _connection = connection;
            _mongoCollection = _connection.GetMongoCollection();
        }

        public async Task CreateProductAsync(ProductRequestDTO productDto)
        {
            try 
            {
                await _mongoCollection.InsertOneAsync(new Product
                (
                    productDto.Name,
                    productDto.Description,
                    productDto.Price,
                    new Category
                    (
                        productDto.Category.ToString(),
                        productDto.Category.Name,
                        productDto.Category.Description
                     )
                ));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in creating product: {ex.Message}");
                throw;
            }
        }

        public async Task<ProductResponseDTO> GetProductByIdAsync(string id)
        {
            try
            {                                               //procure na coleção um produto 'p' onde o Id desse 'p' seja igual ao ID que eu te passei."
                var products = await _mongoCollection.FindAsync(p => p.Id == ObjectId.Parse(id));
                var product = products.FirstOrDefault();

                if (product == null)
                {
                    throw new Exception("Product not found");
                }
                return new ProductResponseDTO
                {
                    Id = product.Id.ToString(),
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Category = new CategoryResponseDTO
                    {
                        Id = product.Category.Id.ToString(),
                        Name = product.Category.Name,
                        Description = product.Category.Description
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in retrieving product by ID: {ex.Message}");
                throw;
            }
        }

        public Task<List<ProductResponseDTO>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
