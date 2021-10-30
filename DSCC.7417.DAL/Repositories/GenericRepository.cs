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

        public GenericRepository(FurnitureDbContext context)
        {
            _context = context;
        }

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

        // this method accepts an id of a specific product and removes it from the table
        public async Task DeleteAsync(int id)
        {
            var product = await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(product);
            await _context.SaveChangesAsync();
        }

        // gets a specific item from the table
        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
        }

        // method for getting all the records from the table
        public async Task<List<T>> GetAllAsync(string category)
        {
            return await _context.Set<T>().Include(category).ToListAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().OrderBy(f => f.Id).ToListAsync();
        }

        // check if an item in table exists
        public bool IfExists(int id)
        {
            return _context.Set<T>().Any(e => e.Id == id);
        }
    }
}
