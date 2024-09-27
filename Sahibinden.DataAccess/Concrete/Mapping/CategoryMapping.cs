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
    public class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(@"Categories", @"dbo");
            builder.HasKey(x => x.Id);
            builder.HasData(
                new Category { Id = 1, Name = "Vasıta", Description = "Vasıta", ParentId = 0 },
                new Category { Id = 2, Name = "Emlak", Description = "Emlak", ParentId = 0 },
                new Category { Id = 3, Name = "Otomobil", Description = "Otomobil", ParentId = 1 },
                new Category { Id = 4, Name = "Arazi, Suv & Pickup", Description = "Arazi, Suv & Pickup", ParentId = 1 },
                new Category { Id = 5, Name = "Motosiklet", Description = "Motosiklet", ParentId = 1 },
                new Category { Id = 6, Name = "Minivan & Panelvan", Description = "Minivan & Panelvan", ParentId = 1 },
                new Category { Id = 7, Name = "Ticari Araçlar", Description = "Ticari Araçlar", ParentId = 1 },
                new Category { Id = 8, Name = "Elektrikli Araçlar", Description = "Elektrikli Araçlar", ParentId = 1 },
                new Category { Id = 9, Name = "Kiralık Araçlar", Description = "Kiralık Araçlar", ParentId = 1 },
                new Category { Id = 10, Name = "Deniz Araçları", Description = "Deniz Araçları", ParentId = 1 },
                new Category { Id = 11, Name = "Hasarlı Araçlar", Description = "Hasarlı Araçlar", ParentId = 1 },
                new Category { Id = 12, Name = "Karavan", Description = "Karavan", ParentId = 1 },
                new Category { Id = 13, Name = "Klasik Araçlar", Description = "Klasik Araçlar", ParentId = 1 },
                new Category { Id = 14, Name = "Hava Araçları", Description = "Hava Araçları", ParentId = 1 },
                new Category { Id = 15, Name = "ATV", Description = "ATV", ParentId = 1 },
                new Category { Id = 16, Name = "UTV", Description = "UTV", ParentId = 1 },
                new Category { Id = 17, Name = "Konut", Description = "Konut", ParentId = 2 },
                new Category { Id = 18, Name = "İş Yeri", Description = "İş Yeri", ParentId = 2 },
                new Category { Id = 19, Name = "Arsa", Description = "Arsa", ParentId = 2 },
                new Category { Id = 20, Name = "Konut Projeleri", Description = "Konut Projeleri", ParentId = 2 },
                new Category { Id = 21, Name = "Bina", Description = "Bina", ParentId = 2 },
                new Category { Id = 22, Name = "Devre Mülk", Description = "Devre Mülk", ParentId = 2 },
                new Category { Id = 23, Name = "Turistik Tesis", Description = "Turistik Tesis", ParentId = 2 },
                new Category { Id = 24, Name = "Daire", Description = "Daire", ParentId = 17 },
                new Category { Id = 25, Name = "Rezidans", Description = "Rezidans", ParentId = 17 },
                new Category { Id = 26, Name = "Müstakil Ev", Description = "Müstakil Ev", ParentId = 17 },
                new Category { Id = 27, Name = "Villa", Description = "Villa", ParentId = 17 },
                new Category { Id = 28, Name = "Çiftlik Evi", Description = "Çiftlik Evi", ParentId = 17 },
                new Category { Id = 29, Name = "Köşk & Konak", Description = "Köşk & Konak", ParentId = 17 },
                new Category { Id = 30, Name = "Yalı", Description = "Yalı", ParentId = 17 },
                new Category { Id = 31, Name = "Yalı Daires,", Description = "Yalı Dairesi", ParentId = 17 },
                new Category { Id = 32, Name = "Yazlık", Description = "Yazlık", ParentId = 17 },
                new Category { Id = 33, Name = "Kooperatif", Description = " Kooperatif", ParentId = 17 }


                );


        }
    }
}
