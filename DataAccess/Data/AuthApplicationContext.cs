using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class AuthApplicationContext : DbContext
    {
        public AuthApplicationContext(DbContextOptions<AuthApplicationContext> options) : base(options) 
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /*var readerRoleId = "06b4a61f-fadd-4cb4-9e4a-7d8aa444d842";
            var writerRoleId = "01f25097-846a-4e70-9e5c-78390b308731";

            var roles = new List<IdentityRole> 
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurencyStamp = readerRoleId,
                    Name = "Reader",
                    
                }
            };*/
        }
    }
}
