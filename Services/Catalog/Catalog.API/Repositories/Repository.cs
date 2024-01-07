using Catalog.API.Data.Interfaces;
using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Catalog.API.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _context;

        public Repository(IMongoCollection<T> context)
        {
            _context = context;
        }

        public async Task Add(T entity)
        {
            await _context.InsertOneAsync(entity);
        }

        public IEnumerable<T> GetAll()
        {
            var filter = Builders<T>.Filter.Empty;
            var result = _context.Find(filter).ToList();
            return result;
        }

        public async Task<T> GetFirstOrDefault(int id)
        {
            var objectId = ObjectId.Parse(id.ToString());
            var filter = Builders<T>.Filter.Eq("_id", objectId);

            return await _context.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<bool> Remove(int id)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", id);
            DeleteResult deleteResult = await _context
                                                .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public Task RemoveRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }


        public async Task<bool> Update(int id, T entity)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            var updateResult = await _context.ReplaceOneAsync(filter, entity);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
