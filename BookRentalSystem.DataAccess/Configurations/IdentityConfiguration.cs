
using BookRentalSystem.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRentalSystem.DataAccess.Configurations
{
    public class IdentityConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "79d3f72f-9d57-4a89-bc84-b294294c70af",
                    Name = Role.SuperAdmin,
                    NormalizedName = Role.SuperAdmin.ToUpper(),
                    ConcurrencyStamp = "0efc5974-a599-4ff6-ac0f-ca95375df2ef"
                },
                new IdentityRole
                {
                    Id = "b3c8f72f-9d57-4a89-bc84-b294294c70af",
                    Name = Role.Admin,
                    NormalizedName = Role.Admin.ToUpper(),
                    ConcurrencyStamp = "1efc5974-a599-4ff6-ac0f-ca95375df2ef"
                },
                new IdentityRole
                {
                    Id = "c4d9g83g-0e68-5b90-cd95-c305305d81bg",
                    Name = Role.User,
                    NormalizedName = Role.User.ToUpper(),
                    ConcurrencyStamp = "2fgd7085-b6aa-5gg7-bd1g-db06486eg3fg"
                }
            );
        }
                
    }
}
