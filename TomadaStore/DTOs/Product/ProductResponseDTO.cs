using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TomadaStore.Models.DTOs.Category;

namespace TomadaStore.Models.DTOs.Product
{
    public class ProductResponseDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; init; }
        [BsonElement("name")]
        public string Name { get; init; }
        [BsonElement("description")]
        public string Description { get; init; }
        [BsonElement("Price")]
        public decimal Price { get; init; }
        [BsonElement("category")]
        public CategoryResponseDTO Category { get; init; }
    }
}
