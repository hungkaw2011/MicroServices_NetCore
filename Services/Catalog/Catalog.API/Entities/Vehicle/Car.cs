namespace Catalog.API.Entities.Vehicle
{
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class Car
    {
        public string Id { get; set; }
        public string Condition { get; set; } // Trình trạng
        public string Brand { get; set; } // Hãng Xe
        public int ManufacturingYear { get; set; } // Năm sản xuất
        public string Version { get; set; } // Phiên bản
        public string TransmissionType { get; set; } // Hộp số
        public string FuelType { get; set; } // Loại nhiên liệu
        public string Origin { get; set; } // Xuất xứ
        public string BodyType { get; set; } // Kiểu dáng
        public int SeatingCapacity { get; set; } // Số chỗ ngồi
        public string Color { get; set; } // Màu sắc
        public decimal Price { get; set; } // Giá bán
        public int KilometersDriven { get; set; }
    }
}
