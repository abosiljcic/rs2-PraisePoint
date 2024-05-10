using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using User.API.Entities;

namespace User.API.Data
{
    public class UserContext : IdentityDbContext<Entities.User>
    {
        public UserContext(DbContextOptions options) : base(options) { }

        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.User>()
                .HasOne<Company>() 
                .WithMany()        
                .HasForeignKey(u => u.CompanyId);

            modelBuilder.Entity<Entities.User>()
                .HasIndex(u => u.CompanyId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
