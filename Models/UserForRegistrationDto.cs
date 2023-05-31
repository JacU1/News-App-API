using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace News_App_API.Models
{
    public class UserForRegistrationDto
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "User tag is required")]
        public string? UserTag { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
