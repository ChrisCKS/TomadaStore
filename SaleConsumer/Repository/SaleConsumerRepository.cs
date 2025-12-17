using MongoDB.Driver;

namespace SaleConsumer.Repository
{
    public class SaleConsumerRepository
    {
        private readonly ILogger<SaleConsumerRepository> _logger;
        private readonly IMongoCollection<SaleConsumerRepository> _collection;

        public SaleConsumerRepository(ILogger<SaleConsumerRepository> logger, IMongoClient client)
        {
            _logger = logger;

            var database = client.GetDatabase("SaleConsumerDB");

            _collection = database.GetCollection<SaleConsumerRepository>("Sales");
        }

        public async Task SaveSaleAsync(SaleConsumerRepository sale)
        {
            try
            {
                await _collection.InsertOneAsync(sale);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while saving sale: {Message}", ex.Message);
                throw;
            }
        }
    }
}
