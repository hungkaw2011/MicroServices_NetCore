using Catalog.API.Data.Interfaces;
using Catalog.API.Entities;
using Catalog.API.Entities.Electronic;
using Catalog.API.Entities.Vehicle;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            var product = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            var phone = database.GetCollection<Phone>("Phones");
            var car = database.GetCollection<Car>("Cars");
            var motorcycle = database.GetCollection<Motorcycle>("Motorcycles");
            CatalogContextSeed.SeedData(product, motorcycle);
            Products = product; // Gán giá trị cho thuộc tính Products
            Phones = phone;
            Cars = car;
            Motorcycles = motorcycle;
        }
        public IMongoCollection<Product> Products { get; }
        public IMongoCollection<Phone> Phones { get; }
        public IMongoCollection<Car> Cars { get; }
        public IMongoCollection<Motorcycle> Motorcycles { get; }
    }
}
