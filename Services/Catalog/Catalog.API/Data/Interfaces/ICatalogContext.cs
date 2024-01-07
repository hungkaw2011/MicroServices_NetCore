using Catalog.API.Entities;
using Catalog.API.Entities.Electronic;
using Catalog.API.Entities.Vehicle;
using MongoDB.Driver;

namespace Catalog.API.Data.Interfaces
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
        IMongoCollection<Phone> Phones { get; }
        IMongoCollection<Car> Cars { get; }
        IMongoCollection<Motorcycle> Motorcycles { get; }
    }
}
