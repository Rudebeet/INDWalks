using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace INDWalks.API.Data
{
    public class INDwalksAuthDbContext : IdentityDbContext
    {
        public INDwalksAuthDbContext(DbContextOptions<INDwalksAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            string readRoleId = "cec2b662-0c49-480c-93d5-bef25845c2e2";
            string writeRoleId = "aea6227-a1e7-466a-8423-ffa7e433625b";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readRoleId,
                    ConcurrencyStamp=readRoleId,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = writeRoleId,
                    ConcurrencyStamp=writeRoleId,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}

