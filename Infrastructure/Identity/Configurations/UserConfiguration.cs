using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Infrastructure.Identity.Models;

namespace Infrastructure.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(new ApplicationUser 
            {
                Id = "e5a5ed06-c0c5-4af1-8b87-7495863a089d",
                Email = "admin@localhost.com",
                NormalizedEmail = "admin@localhost.com",
                Nombre = "Admin",
                Apellido = "sa",
                UserName = "admin",
                NormalizedUserName = "admin",
                PasswordHash = hasher.HashPassword(null, "admin@sa"),
                EmailConfirmed = true
            });
        }
    }
}