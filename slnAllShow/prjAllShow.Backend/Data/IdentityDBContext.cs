using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using prjAllShow.Backend.Models.Identity;

namespace prjAllShow.Backend.Data
{
    public class IdentityDBContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public IdentityDBContext(DbContextOptions<IdentityDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            //string schema = "AspNetIdentity";

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "AllShowUsers"/*, schema: schema*/);
            });

            builder.Entity<IdentityUserClaim<int>>(entity =>
            {
                entity.ToTable("AllShowUserClaims"/*, schema: schema*/);
            });

            builder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.ToTable("AllShowUserLogins"/*, schema: schema*/);
            });

            builder.Entity<IdentityUserToken<int>>(entity =>
            {
                entity.ToTable("AllShowUserTokens"/*, schema: schema*/);
            });

            builder.Entity<ApplicationRole>(entity =>
            {
                entity.ToTable(name: "AllShowRoles"/*, schema: schema*/);
            });
          
            builder.Entity<IdentityRoleClaim<int>>(entity =>
            {
                entity.ToTable("AllShowRoleClaims"/*, schema: schema*/);
            });

            builder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.ToTable("AllShowUserRoles"/*, schema: schema*/);
            });

            
        }
    }
}
