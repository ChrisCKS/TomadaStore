using Microsoft.AspNetCore.Connections;
using PaymentValidAPI.Service.Interface;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;
using TomadaStore.Models.Models;

namespace PaymentValidAPI.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly ILogger<PaymentService> _logger;
        private readonly ConnectionFactory _connectionFactory;

        public PaymentService(ILogger<PaymentService> logger, ConnectionFactory connectionFactory)
        {
            _logger = logger;
            _connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
        }

        public async Task ProcessPaymentAsync()
        {
            try
            {
                using var connection = await _connectionFactory.CreateConnectionAsync();

                using var channel = await connection.CreateChannelAsync();

                await channel.QueueDeclareAsync(queue: "saleQueue",
                                                 durable: false,
                                                 exclusive: false,
                                                 autoDelete: false,
                                                 arguments: null);

                var consumer = new AsyncEventingBasicConsumer(channel);

                consumer.ReceivedAsync += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();

                    var message = System.Text.Encoding.UTF8.GetString(body);

                    var sale = JsonSerializer.Deserialize<Sale>(message);

                    await ValidatePaymentAsync(sale);
                };

                await channel.BasicConsumeAsync(queue: "saleQueue",
                                                 autoAck: true,
                                                 consumer: consumer);

            }
            catch (Exception ex)
            {
                _logger.LogError("Error while processing payment: {Message}", ex.Message);
                throw;
            }
        }

        public async Task ValidatePaymentAsync(Sale sale) 
        {
            var connection = await _connectionFactory.CreateConnectionAsync();

            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "paymentValidationQueue",
                                             durable: false,
                                             exclusive: false,
                                             autoDelete: false,
                                             arguments: null);

            string status;
            if (sale.TotalPrice >= 500) 
            {
                status = "Approved";
            }
            else 
            {
                status = "Declined";
            }

            var paymentResult = new
            {
                SaleId = sale.Id,
                Status = status,
                TotalPrice = sale.TotalPrice
            };

            var saleString = JsonSerializer.Serialize(paymentResult);

            var body = System.Text.Encoding.UTF8.GetBytes(saleString);

            await channel.BasicPublishAsync(exchange: string.Empty,
                                              routingKey: "paymentValidationQueue",
                                              body: body);

        }
    }   
}
