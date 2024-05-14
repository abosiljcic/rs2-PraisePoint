using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace User.API.Data.EntityTypeConfigurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
            new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
            },
            new IdentityRole
                {
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE",
                }
            );
        }
    }
}
