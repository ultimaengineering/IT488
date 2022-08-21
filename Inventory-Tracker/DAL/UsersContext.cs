﻿using Inventory_Tracker.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Tracker.DAL
{
    public class UsersContext : DbContext
    {

        protected readonly IConfiguration Configuration;

        public UsersContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
        }
    }
}