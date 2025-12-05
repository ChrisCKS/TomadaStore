using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.SaleAPI.Model;
using TomadaStore.SaleAPI.Repository.Interface;
using TomadaStore.SaleAPI.Service.Interface;

namespace TomadaStore.SaleAPI.Service
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ILogger<SaleService> _logger;

        private readonly HttpClient _httpClientProduct;
        private readonly HttpClient _httpClientCustomer;

        public SaleService(
            ISaleRepository saleRepository,
            ILogger<SaleService> logger,
            HttpClient httpProduct,
            HttpClient httpCustomer)                    
        {
            _saleRepository = saleRepository;
            _logger = logger;
            _httpClientProduct = httpProduct;
            _httpClientCustomer = httpCustomer;
        }

        public async Task CreateSaleAsync(int idCustomer, string idProduct, SaleRequestDTO saleDTO)
        {
            try
            {
                var customer = await _httpClientCustomer.GetFromJsonAsync<CustomerResponseDTO>(idCustomer.ToString());

                var product = await _httpClientProduct.GetFromJsonAsync<ProductResponseDTO>(idProduct);

                await _saleRepository.CreateSaleAsync(customer, product, saleDTO);
            }
            catch (Exception ex) 
            {
                _logger.LogError("Error while creating sale: {Message}", ex.Message);
                throw;
            }
        }
    }
}
