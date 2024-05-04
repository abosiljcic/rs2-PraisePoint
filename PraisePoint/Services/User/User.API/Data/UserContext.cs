using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using User.API.Entities;

namespace User.API.Data
{
    public class UserContext : IdentityDbContext<Entities.User>
    {
        public UserContext(DbContextOptions options) : base(options) { }
    }
}
