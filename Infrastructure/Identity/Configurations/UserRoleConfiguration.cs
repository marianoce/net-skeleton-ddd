using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Identity.Models;

namespace Infrastructure.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(new IdentityUserRole<string> 
            {
                RoleId = "c441a447-c130-40bc-8d3f-fad9ecb76937",
                UserId = "e5a5ed06-c0c5-4af1-8b87-7495863a089d"
            });
        }
    }
}