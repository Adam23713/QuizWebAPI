using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace DataAccess.Data
{
    public class AuthApplicationContext : IdentityDbContext
    {
        //public DbSet<IdentityUser> IdentityUsers { get; set; }

        public AuthApplicationContext(DbContextOptions<AuthApplicationContext> options) : base(options) 
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var gamerRoleId = "06b4a61f-fadd-4cb4-9e4a-7d8aa444d842";
            var adminRoleId = "01f25097-846a-4e70-9e5c-78390b308731";

            var roles = new List<IdentityRole> 
            {
                new()
                {
                    Id = gamerRoleId,
                    ConcurrencyStamp = gamerRoleId,
                    Name = "Gamer",
                    NormalizedName = "Gamer".ToUpper()
                },
                new() {
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
