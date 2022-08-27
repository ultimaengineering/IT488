using Inventory_Tracker.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Tracker.DAL
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {

        protected readonly IConfiguration Configuration;

        public DbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Inventory> Inventory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
        }
    }
}
