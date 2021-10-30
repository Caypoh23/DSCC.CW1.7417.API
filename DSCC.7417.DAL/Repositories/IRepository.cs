using DSCC._7417.DAL.DBO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DSCC._7417.DAL.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync(string propertyName);
        Task<List<T>> GetAllAsync();
        bool IfExists(int id);
    }
}
