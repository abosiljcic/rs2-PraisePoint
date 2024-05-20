using System.ComponentModel.DataAnnotations;

namespace User.API.DTOs
{
    public class UserCredentialsDto
    {
        [Required(ErrorMessage = "User name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password name is required")]
        public string Password { get; set; }
    }
}
