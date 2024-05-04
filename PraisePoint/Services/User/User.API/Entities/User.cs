using Microsoft.AspNetCore.Identity;

namespace User.API.Entities
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public int CompanyId { get; set; }
        public string ImageUrl { get; set; }
        public bool Active { get; set; }
    }
}
