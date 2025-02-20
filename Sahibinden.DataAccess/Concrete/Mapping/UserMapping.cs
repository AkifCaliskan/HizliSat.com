using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sahibinden.Entities.Concrete;
using Sahibinden.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.DataAccess.Concrete.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(@"Users", @"dbo");
            builder.HasKey(x => x.Id);
            var hashedPass = 
            builder.HasData(
                new User { Id = 1, Status = true, UserType= UserType.SuperAdmin , FirstName = "Akif", LastName = "Çalışkan", Email = "akifcaliskan@gmail.com", Phone = "5555555555", Address = "Ankara", Password = "1234" }
                );

        }
    }
}
