using Catalog.API.Data;
using Catalog.API.Data.Interfaces;
using Catalog.API.Entities;
using Catalog.API.Entities.Vehicle;
using Catalog.API.Repositories.Interfaces;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Catalog.API.Repositories
{
    public class MotorcycleRepository : Repository<Motorcycle>, IMotorcycleRepository
    {
        public MotorcycleRepository(IMongoCollection<Motorcycle> context) : base(context)
        {
        }
    }
}
