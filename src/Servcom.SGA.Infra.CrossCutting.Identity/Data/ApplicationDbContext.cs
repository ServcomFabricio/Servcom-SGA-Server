using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Servcom.SGA.Infra.CrossCutting.Identity.Models;

namespace Servcom.SGA.Infra.CrossCutting.Identity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>(entity => {
                entity.Property(m => m.Id).HasMaxLength(110);
                entity.Property(m => m.Email).HasMaxLength(110);
                entity.Property(m => m.NormalizedEmail).HasMaxLength(110);
                entity.Property(m => m.NormalizedUserName).HasMaxLength(110);
                entity.Property(m => m.UserName).HasMaxLength(110);
            });
            modelBuilder.Entity<IdentityRole>(entity => {
                entity.Property(m => m.Id).HasMaxLength(110);
                entity.Property(m => m.Name).HasMaxLength(110);
                entity.Property(m => m.NormalizedName).HasMaxLength(110);
            });
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.Property(m => m.LoginProvider).HasMaxLength(110);
                entity.Property(m => m.ProviderKey).HasMaxLength(110);
            });
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.Property(m => m.UserId).HasMaxLength(110);
                entity.Property(m => m.RoleId).HasMaxLength(110);
            });
            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.Property(m => m.UserId).HasMaxLength(110);
                entity.Property(m => m.LoginProvider).HasMaxLength(110);
                entity.Property(m => m.Name).HasMaxLength(110);

            });



        }

    }
}
