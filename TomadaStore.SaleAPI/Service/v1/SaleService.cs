using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.SaleAPI.Model;
using TomadaStore.SaleAPI.Repository.Interface;
using TomadaStore.SaleAPI.Service.v1.Interface;

namespace TomadaStore.SaleAPI.Service.v1
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ILogger<SaleService> _logger;

        private readonly IHttpClientFactory _httpClientFactory;

        public SaleService(
            ISaleRepository saleRepository,                         
            ILogger<SaleService> logger,
            IHttpClientFactory httpClientFactory)       /**/
        {
            _saleRepository = saleRepository;
            _logger = logger;
            _httpClientFactory = httpClientFactory;     /**/
        }

        public async Task CreateSaleAsync(int idCustomer, SaleRequestDTO saleDTO)
        {
            try
            {

                var clientCustomer = _httpClientFactory.CreateClient("CustomerApi");
                var clientProduct = _httpClientFactory.CreateClient("ProductApi");

                var customer = await clientCustomer.GetFromJsonAsync<CustomerResponseDTO>($"{idCustomer}");

                var productsList = new List<ProductResponseDTO>();
                decimal totalPrice = 0;

                foreach (var productId in saleDTO.ProductsIds)
                {
                    var product = await clientProduct.GetFromJsonAsync<ProductResponseDTO>($"{productId}");

                    if (product != null)
                    {
                        productsList.Add(product);
                        totalPrice += product.Price;
                    }
                }

                await _saleRepository.CreateSaleAsync(customer, productsList, totalPrice);
            }
            catch (Exception ex) 
            {
                _logger.LogError("Error while creating sale: {Message}", ex.Message);
                throw;
            }
        }
    }
}
