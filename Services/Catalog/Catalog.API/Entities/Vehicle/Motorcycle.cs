using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Catalog.API.Entities.Vehicle
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class Motorcycle
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Status { get; set; } // Trạng thái (Used)
        public Vehicle Vehicle { get; set; } // Phương tiện
        public string VehicleType { get; set; } // Loại phương tiện
        public int EngineCapacity { get; set; } // Dung tích động cơ
        public string LicensePlate { get; set; } // Biển số xe
        public string BodyType { get; set; } // Kiểu dáng
        public string Version { get; set; } // Phiên bản
        public string Color { get; set; } // Màu sắc
        public int KilometersDriven { get; set; } // Số km đã đi
        public long Price { get; set; } // Giá bán
        public MediaInfo MediaInfo { get; set; } // Thông tin phương tiện
        public ListingInfo ListingInfo { get; set; } // Thông tin đăng bán
        public string Category { get; set; } // Danh mục

    }
}
