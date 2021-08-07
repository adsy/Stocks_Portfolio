using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Configurations;
using Services.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Services.Configurations.Entities;

namespace Services.Data
{
    // DbContext - class which is inherited from to allow communication to the db and its tables.
    // IdentityDbContext - class which is inherited from which allows user identity core features for auth
    public class StockDbContext : IdentityDbContext<ApiUser>
    {
        public StockDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<PortfolioTracker> PortfolioTrackers { get; set; }
        public DbSet<Cryptocurrency> Cryptocurrencies { get; set; }
        public DbSet<ApiUser> AspNetUsers { get; set; }
        public DbSet<SoldInstrument> SoldInstruments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Example of exporting the HasData configuration files
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new StockConfiguration());
            builder.ApplyConfiguration(new HourlyPortfolioTrackerConfiguration());
        }
    }
}