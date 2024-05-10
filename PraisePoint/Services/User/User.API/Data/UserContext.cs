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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Entities.User>()
                .HasOne<Company>() 
                .WithMany()        
                .HasForeignKey(u => u.CompanyId); 
        }

    }
}
