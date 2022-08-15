using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Identity.Models;

namespace Infrastructure.Identity.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(new IdentityRole {
                Id = "c441a447-c130-40bc-8d3f-fad9ecb76937",
                Name = "Administrator"
            });
        }
    }
}