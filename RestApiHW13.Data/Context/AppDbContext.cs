using Microsoft.EntityFrameworkCore;
using RestApiHW13.Data.Entites;

namespace RestApiHW13.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public virtual DbSet<Product> Products { get; set; }
    }
}
