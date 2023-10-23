using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace News_App_API.Models
{
	public class UserAuthDto
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is required.")]
        public string? Email { get; set; }
        [PasswordPropertyText]
        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }
        public string? RefreshToken { get; set; }
    }
}

