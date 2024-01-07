#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Catalog.API.Entities.Vehicle
{
    public class Vehicle
    {
        [BsonElement("Name")] // Ánh xạ thuộc tính Name vào trường có tên "name" trong MongoDB
        public  string Name { get; set; }
        public string Brand { get; set; } // Thương hiệu
        public int ManufacturingYear { get; set; } // Năm sản xuất
        public string Origin { get; set; } // Xuất xứ
        public string TransmissionType { get; set; } // Loại hộp số
        public string FuelType { get; set; } // Loại nhiên liệu
        public int SeatingCapacity { get; set; } // Số chỗ ngồi
    }
}
