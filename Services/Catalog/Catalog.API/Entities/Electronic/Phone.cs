using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Catalog.API.Entities.Electronic
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
{
    public class Phone
    {
        [BsonId] //Key 
        [BsonRepresentation(BsonType.ObjectId)] //Xác định cách biểu diễn của thuộc tính khi ánh xạ vào BSON
        public string Id { get; set; }
        [BsonElement("Name")] // Ánh xạ thuộc tính Name vào trường có tên "name" trong MongoDB
        public string Name { get; set; }
        public string Category { get; set; } // Loại
        public string Address { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public string Condition { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public string Model { get; set; }
        public DateTime WarrantyExpiration { get; set; }
        public string Origin { get; set; }
        public decimal Price { get; set; }
        public string AdTitle { get; set; }
        public string DetailedDescription { get; set; }
    }
}
