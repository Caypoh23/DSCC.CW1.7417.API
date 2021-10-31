using DSCC._7417.DAL.Context;
using DSCC._7417.DAL.DBO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSCC._7417.DAL.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly FurnitureDbContext _context;

        public GenericRepository(FurnitureDbContext context) => _context = context;

        public async Task AddAsync(T entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
        }

        // editing functionality
        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // this method accepts an id of a specific item and removes it from the table
        public async Task DeleteAsync(T item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        #region Get by Id
        // gets a specific item from the table
        public async Task<T> GetByIdAsync(int id) =>  await _context.Set<T>().FindAsync(id);

        // get a specific item including category
        public async Task<T> GetByIdAsync(int id, string category) => await _context.Set<T>().Include(category).SingleOrDefaultAsync(f => f.Id == id);
        #endregion

        #region Get all 
        // method for getting all the records from the table including category
        public async Task<List<T>> GetAllAsync(string category) => await _context.Set<T>().Include(category).ToListAsync();

        // method overrive for GetAllAsync(string category)
        public async Task<List<T>> GetAllAsync() => await _context.Set<T>().OrderBy(f => f.Id).ToListAsync();
        #endregion

        // check if an item in table exists
        public bool IfExists(int id) => _context.Set<T>().Any(e => e.Id == id);

    }
}
