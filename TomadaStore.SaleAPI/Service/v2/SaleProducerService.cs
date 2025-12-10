using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.Models;
using TomadaStore.SaleAPI.Model;
using TomadaStore.SaleAPI.Service.v2.Interface;

namespace TomadaStore.SaleAPI.Service.v2
{
    public class SaleProducerService : ISaleProduceService
    {
        private readonly ILogger<SaleProducerService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ConnectionFactory _connectionFactory;

        public SaleProducerService(
           ILogger<SaleProducerService> logger,
           IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;

            //conexão com Rabbit
            _connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
        }

        public async Task <Sale> CreateSaleAsync(int idCostumer, List<string> idsProduct)
        {
            try
            {
                var httpClientCustomer = _httpClientFactory.CreateClient("Customer");

                var customer = await httpClientCustomer.GetFromJsonAsync<CustomerResponseDTO>(idCostumer.ToString());

                var saleCustomer = new Customer
                (
                    customer.Id,
                    customer.FirstName,
                    customer.LastName,
                    customer.Email,
                    customer.PhoneNumber,
                    customer.Status
                );

                var httpClientProduct = _httpClientFactory.CreateClient("Product");

                var products = new List<Product>();
                decimal total = 0;

                foreach (var idProduct in idsProduct)
                {
                    var product = await httpClientProduct.GetFromJsonAsync<ProductResponseDTO>(idProduct);

                    var productSale = new Product
                    (
                        product.Id,
                        product.Name,
                        product.Description,
                        product.Price,
                        new Category(product.Category.Id, product.Category.Name, product.Category.Description)
                    );

                    products.Add(productSale);
                    total += product.Price;
                }

                return new Sale(saleCustomer, products, total);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating sale.");
                throw;
            }
        }

        public async Task SaleProducerSendAsync(Sale sale)
        {
            try
            {
                using var connection = await _connectionFactory.CreateConnectionAsync();

                using var channel = await connection.CreateChannelAsync();    //envio mensagens pelo canal

                //declaro a fila
                await channel.QueueDeclareAsync(queue: "saleQueue",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                var saleMessage = JsonSerializer.Serialize(sale); //converto o objeto VENDA em string json

                var body = Encoding.UTF8.GetBytes(saleMessage); //converto a string em array de bytes

                //envio a mensagem na fila
                await channel.BasicPublishAsync(exchange: string.Empty,
                                             routingKey: "saleQueue",
                                             body: body);

                _logger.LogInformation(" [x] Sent message to queue {Queue}: {Message}", "saleQueue", saleMessage);
            }
            catch (Exception ex) 
            {
                _logger.LogError("Error sending sale to queue: " + ex.Message);
                throw;
            }
        }  
    }
}
