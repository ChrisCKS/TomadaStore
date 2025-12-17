using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using TomadaStore.Models.DTOs.Payment;

namespace SaleConsumer.Service
{
    public class SaleConsumerService
    {
        private readonly ILogger<SaleConsumerService> _logger;
        //private readonly IHttpClientFactory _httpClientFactory;
        private readonly ConnectionFactory _connectionFactory;

        public SaleConsumerService(ILogger<SaleConsumerService> logger)
        {
            _logger = logger;

            //conexão com Rabbit
            _connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
        }

        public async Task SaleConsumerAsync()
        {
            try
            {
                using var connection = await _connectionFactory.CreateConnectionAsync();

                using var channel = await connection.CreateChannelAsync();  

                await channel.QueueDeclareAsync(queue: "paymentValidationQueue",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                var consumer = new AsyncEventingBasicConsumer(channel);

                //este objeto é ativado quando uma mensagem é recebida na fila
                consumer.ReceivedAsync += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();   //pega os bytes da mensagem

                    var message = Encoding.UTF8.GetString(body);    //converte os bytes em string

                    var paymentResult = JsonSerializer.Deserialize<PaymentRequestDTO>(message); //converte a string em objeto VENDA

                    Console.WriteLine($" [x] Received {message}");
                };

                //inicia o consumo da fila
                await channel.BasicConsumeAsync(
                                                queue: "paymentValidationQueue",
                                                autoAck: true,
                                                consumer: consumer);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error processing message: " + ex.Message);
            }
        }
    }
}
