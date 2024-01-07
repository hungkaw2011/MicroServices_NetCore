using Catalog.API.Entities;
using Catalog.API.Entities.Vehicle;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection, IMongoCollection<Motorcycle> motorcycleCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetPreconfiguredProducts());
                motorcycleCollection.InsertManyAsync(GetPreconfiguredMotorcycles());
            }
        }
        private static IEnumerable<Motorcycle> GetPreconfiguredMotorcycles()
        {
            return new List<Motorcycle>()
            {
                new Motorcycle
                {
                    Id = "6599e51ecd7b92b2400dc874",
                    Status = "Used",
                    Vehicle = new Vehicle
                    {
                        Name = "Honda CBR500R",
                        Brand = "Honda",
                        ManufacturingYear = 2020,
                        Origin = "Japan",
                        TransmissionType = "Manual",
                        FuelType = "Gasoline",
                        SeatingCapacity = 2
                    },
                    VehicleType = "",
                    EngineCapacity = 0,
                    LicensePlate = "",
                    BodyType = "Sport",
                    Version = "CBR500R",
                    Color = "Red",
                    KilometersDriven = 5000,
                    Price = 70000000,
                    
                    MediaInfo = new MediaInfo
                    {
                        ImageUrl = "https://example.com/honda_cbr500r.jpg",
                        VideoUrl = "https://example.com/honda_cbr500r.mp4"
                    },
                    ListingInfo = new ListingInfo
                    {
                        Title = "Honda CBR500R for Sale",
                        Description = "Powerful sportbike with great handling."
                    },
                    Category = "Sportbike"
                },
                new Motorcycle
                {
                    Id = "6599e51ecd7b92b2400dc875",
                    Status = "New",
                    Vehicle = new Vehicle
                    {
                        Name = "Yamaha MT-09",
                        Brand = "Yamaha",
                        ManufacturingYear = 2021,
                        Origin = "Japan",
                        TransmissionType = "Manual",
                        FuelType = "Gasoline",
                        SeatingCapacity = 2
                    },
                    VehicleType = "",
                    EngineCapacity = 0,
                    LicensePlate = "",
                    BodyType = "Naked",
                    Version = "MT-09",
                    Color = "Blue",
                    KilometersDriven = 0,
                    Price = 70000000,
                    MediaInfo = new MediaInfo
                    {
                        ImageUrl = "https://example.com/yamaha_mt09.jpg",
                        VideoUrl = "https://example.com/yamaha_mt09.mp4"
                    },
                    ListingInfo = new ListingInfo
                    {
                        Title = "Yamaha MT-09 for Sale",
                        Description = "Naked bike with a powerful three-cylinder engine."
                    },
                    Category = "Naked Bike"
                }
            };
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Name = "IPhone X",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "product-1.png",
                    Price = 950.00M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f6",
                    Name = "Samsung 10",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "product-2.png",
                    Price = 840.00M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f7",
                    Name = "Huawei Plus",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "product-3.png",
                    Price = 650.00M,
                    Category = "White Appliances"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f8",
                    Name = "Xiaomi Mi 9",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "product-4.png",
                    Price = 470.00M,
                    Category = "White Appliances"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f9",
                    Name = "HTC U11+ Plus",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "product-5.png",
                    Price = 380.00M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47fa",
                    Name = "LG G7 ThinQ",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "product-6.png",
                    Price = 240.00M,
                    Category = "Home Kitchen"
                }
            };
        }
    }
}
