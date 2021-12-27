using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Context
{
    public class SgiContext : DbContext 
    {
        public DbSet<User> Users {get; set;}
        public DbSet<Apartment> Apartments {get; set;}
        public DbSet<Check> Checks {get; set;}
        public DbSet<Photo> Photos {get; set;}
        public DbSet<Rental> Rentals {get; set;}
        public DbSet<SessionUser> Sessions {get; set;}
        public DbSet<Element> Elements {get; set;}

        public SgiContext (){}

        public SgiContext (DbContextOptions options): base (options) {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                string directory = System.IO.Directory.GetCurrentDirectory();
                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(directory)
                .AddJsonFile("appsettings.json")
                .Build();
                var connectionString = configuration.GetConnectionString(@"SgiDB");
                optionsBuilder.UseSqlServer(connectionString).UseLazyLoadingProxies();

            }
        }
    }
}