﻿using Microsoft.AspNetCore.Identity;

namespace User.API.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public Guid CompanyId { get; set; }
        public string ImageUrl { get; set; }
        public bool Active { get; set; }
    }
}