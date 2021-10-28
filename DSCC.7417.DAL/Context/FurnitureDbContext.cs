using DSCC._7417.DAL.DBO;
using Microsoft.EntityFrameworkCore;

namespace DSCC._7417.DAL.Context
{
    public class FurnitureDbContext : DbContext
    {
        public FurnitureDbContext(DbContextOptions<FurnitureDbContext> options)
            : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
