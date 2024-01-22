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

            var readerRoleId = "06b4a61f-fadd-4cb4-9e4a-7d8aa444d842";
            var writerRoleId = "01f25097-846a-4e70-9e5c-78390b308731";

            var roles = new List<IdentityRole> 
            {
                new()
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new() {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
