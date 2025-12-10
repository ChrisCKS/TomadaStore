using MongoDB.Driver;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.Models;
using TomadaStore.SaleAPI.Data;
using TomadaStore.SaleAPI.Model;
using TomadaStore.SaleAPI.Repository.Interface;

namespace TomadaStore.SaleAPI.Repository
{
    public class SaleRepository : ISaleRepository
    {
        private readonly ILogger<SaleRepository> _logger;
        private readonly IMongoCollection<Sale> _mongoCollection;
        private readonly ConnectionDB _connection;

        public SaleRepository(ILogger<SaleRepository> logger, ConnectionDB connection)
        {
            _logger = logger;
            _connection = connection;
            _mongoCollection = _connection.GetMongoCollection();
        }

        public async Task CreateSaleAsync(CustomerResponseDTO customerDTO, List<ProductResponseDTO> productsDTO, decimal totalPrice)    /**/
        {
            try
            {
                var customer = new Customer
                (                                                           
                    customerDTO.FirstName,                                                                                 
                    customerDTO.LastName,   
                    customerDTO.Email,
                    customerDTO.Status
                );

                var listProducts = new List<Product>();

                foreach (var pDTO in productsDTO)
                {
                    var category = new Category
                    (
                        pDTO.Category.Id,
                        pDTO.Category.Name,
                        pDTO.Category.Description
                    );
                    var product = new Product
                        (
                            pDTO.Id,
                            pDTO.Name,
                            pDTO.Description,
                            pDTO.Price,
                            category
                        );
                    listProducts.Add(product);
                }

                var sale = new Sale(customer, listProducts, totalPrice);

                await _mongoCollection.InsertOneAsync(sale);                                                        /**/
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in creating product: {ex.Message}");
                throw;
            }
        }

    }
}
