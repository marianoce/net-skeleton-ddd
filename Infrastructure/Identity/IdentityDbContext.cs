using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Identity.Models;
using Infrastructure.Identity.Configurations;

namespace Infrastructure.Identity
{
    public class IdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration()); 
        }
    }
}