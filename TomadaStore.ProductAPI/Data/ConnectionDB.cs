using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TomadaStore.Models.Models;

namespace TomadaStore.ProductAPI.Data
{
    public class ConnectionDB
    {
        public readonly IMongoCollection<Product> mongoCollection;

        public ConnectionDB(IOptions<MongoDbSettings> mongoDbSettings)
        {
            MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
            mongoCollection = database.GetCollection<Product>(mongoDbSettings.Value.CollectionName);
        }

        public IMongoCollection<Product> GetMongoCollection()
        {
            return mongoCollection;
        }
    }
}
