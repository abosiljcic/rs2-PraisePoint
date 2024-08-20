using System.ComponentModel.DataAnnotations;

namespace User.API.DTOs
{
    public class NewUserDto
    {
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        public string ImageUrl { get; set; }
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "CompanyId is required")]
        public Guid CompanyId { get; set; }
    }
}
