using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using User.API.Data.EntityTypeConfigurations;
using User.API.Entities;

namespace User.API.Data
{
    public class UserContext : IdentityDbContext<Entities.User>
    {
        public UserContext(DbContextOptions options) : base(options) { }

        public DbSet<Company> Companies { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Entities.User>()
                .HasOne<Company>() 
                .WithMany()        
                .HasForeignKey(u => u.CompanyId);

            modelBuilder.Entity<Entities.User>()
                .HasIndex(u => u.CompanyId);

            modelBuilder.Entity<Entities.User>()
            .Property(u => u.Active)
            .HasDefaultValue(true);

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

    }
}
