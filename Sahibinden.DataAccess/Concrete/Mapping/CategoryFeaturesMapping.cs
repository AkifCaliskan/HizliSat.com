using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sahibinden.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.DataAccess.Concrete.Mapping
{
    public class CategoryFeaturesMapping : IEntityTypeConfiguration<CategoryFeature>
    {
        public void Configure(EntityTypeBuilder<CategoryFeature> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable(@"CategoryFeatures", @"dbo");
            builder.HasData(
                new CategoryFeature { Id = 1, Key = "elevator", Name = "Asansör", CategoryId = 1 },
                new CategoryFeature { Id = 2, Key = "numberOfFloor", Name = "Bulunduğu Kat", CategoryId = 1 },
                new CategoryFeature { Id = 3, Key = "numberOfRoom", Name = "Oda Sayısı", CategoryId = 1 },
                new CategoryFeature { Id = 4, Key = "m2", Name = "M2", CategoryId = 1 },
                new CategoryFeature { Id = 5, Key = "buildingAge", Name = "Bina Yaşı", CategoryId = 1 },
                new CategoryFeature { Id = 6, Key = "floor", Name = "Bulunduğu Kat", CategoryId = 1 },
                new CategoryFeature { Id = 7, Key = "heating", Name = "Isıtma", CategoryId = 1 },
                new CategoryFeature { Id = 8, Key = "balcony", Name = "Balkon", CategoryId = 1 },
                new CategoryFeature { Id = 9, Key = "parking", Name = "Otopark", CategoryId = 1 },

                new CategoryFeature { Id = 10, Key = "brand", Name = "Marka", CategoryId = 2 },
                new CategoryFeature { Id = 11, Key = "serial", Name = "Seri", CategoryId = 2 },
                new CategoryFeature { Id = 12, Key = "model", Name = "Model", CategoryId = 2 },
                new CategoryFeature { Id = 13, Key = "age", Name = "Yıl", CategoryId = 2 },
                new CategoryFeature { Id = 14, Key = "gear", Name = "Vites", CategoryId = 2 },
                new CategoryFeature { Id = 15, Key = "km", Name = "KM", CategoryId = 2 },
                new CategoryFeature { Id = 16, Key = "caseType", Name = "Kasa Tipi", CategoryId = 2 },
                new CategoryFeature { Id = 17, Key = "engineSize", Name = "Motor Hacmi", CategoryId = 2 },
                new CategoryFeature { Id = 18, Key = "enginePower", Name = "Motor Gücü", CategoryId = 2 },
                new CategoryFeature { Id = 19, Key = "traction", Name = "Çekiş", CategoryId = 2 },
                new CategoryFeature { Id = 20, Key = "color", Name = "Renk", CategoryId = 2 }
                );


        }
    }
}
