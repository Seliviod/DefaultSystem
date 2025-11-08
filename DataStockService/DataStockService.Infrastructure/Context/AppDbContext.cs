using DataStockService.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataStockService.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<OutboxEvent> OutboxEvents { get; set; }
    }
}
