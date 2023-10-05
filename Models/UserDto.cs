using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace News_App_API.Models
{
    public class UserDto
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public string? LastName { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "User tag is required")]
        public string? UserTag { get; set; }
        public IList<ArticleDto> Articles { get; } = new List<ArticleDto>();
        public IList<CommentDto> Comments { get; } = new List<CommentDto>();
        public IList<RatingDto> Ratings { get; } = new List<RatingDto>();
    }
}
