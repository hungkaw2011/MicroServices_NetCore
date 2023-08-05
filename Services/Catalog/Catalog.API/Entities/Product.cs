using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Catalog.API.Entities
{
    public class Product
    {
        [BsonId] //Key 
        [BsonRepresentation(BsonType.ObjectId)] //Xác định cách biểu diễn của thuộc tính khi ánh xạ vào BSON
        public string Id { get; set; }

        [BsonElement("Name")] // Ánh xạ thuộc tính Name vào trường có tên "name" trong MongoDB
        public string Name { get; set; }
        public string Category { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public decimal Price { get; set; }
    }
}
