using Catalog.API.Entities;
using System.Linq.Expressions;

namespace Catalog.API.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetFirstOrDefault(int id);
        IEnumerable<T> GetAll();
        Task Add(T entity);
        Task<bool> Remove(int id);
        Task RemoveRange(IEnumerable<T> entities);
        Task<bool> Update(int id, T entity);
    }
}
