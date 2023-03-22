using Catalog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Catalog
{
    public class WandsContext : DbContext
    {
        public WandsContext(DbContextOptions<WandsContext> options): base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Wand> Wands { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
