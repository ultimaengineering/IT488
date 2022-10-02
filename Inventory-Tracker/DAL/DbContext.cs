using Inventory_Tracker.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Tracker.DAL
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {

        protected readonly IConfiguration Configuration;

        public DbContext(IConfiguration configuration) => Configuration = configuration;

        public DbSet<User>? Users { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Sale>? Sales { get; set; }
        public DbSet<SalesSummary>? SalesSummaries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
            options.EnableDetailedErrors();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SalesSummary>()
                .ToView("sales_summary_view")
                .HasKey(t => t.closing_month_name);
        }
    }
}
