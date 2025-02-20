using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sahibinden.DataAccess.Concrete.Mapping;
using Sahibinden.Entities.Concrete;
using System;

namespace Sahibinden.DataAccess.Concrete
{
    public class Context : DbContext
    {
        public Context()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"));

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Advert> Adverts { get; set; }
        public DbSet<AdvertDetail> AdvertDetails { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<CategoryFeature> CategoryFeatures { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new AdvertMapping());
            modelBuilder.ApplyConfiguration(new AdvertDetailMapping());
            modelBuilder.ApplyConfiguration(new CategoryMapping());
            modelBuilder.ApplyConfiguration(new ImageMapping());
            modelBuilder.ApplyConfiguration(new CategoryFeaturesMapping());

            
        }

        
    }
}
