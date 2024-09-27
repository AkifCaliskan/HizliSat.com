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
    public class AdvertDetailMapping : IEntityTypeConfiguration<AdvertDetail>
    {
        public void Configure(EntityTypeBuilder<AdvertDetail> builder)
        {
            builder.ToTable(@"AdvertDetails", @"dbo");
            builder.HasKey(x => x.Id);

        }
    }
}
