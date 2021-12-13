using Microsoft.EntityFrameworkCore;
using prjAllShow.Backend.Models;

namespace prjAllShow.Backend.Data
{
    public class AllShowDBContext : DbContext
    {
        public AllShowDBContext(DbContextOptions<AllShowDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<DbFiles> DbFiles { get; set; }
    }
}
