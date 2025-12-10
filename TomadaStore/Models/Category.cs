using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomadaStore.Models.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public Category(string id, string name, string description)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Name = name;
            Description = description;
        }
    }
}
