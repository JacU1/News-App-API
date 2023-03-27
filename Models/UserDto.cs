using Microsoft.Extensions.Hosting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace News_App_API.Models
{
    public class UserDto
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? UserTag { get; set; }
        public IList<ArticleDto> Articles { get; } = new List<ArticleDto>();
        public IList<CommentDto> Comments { get; } = new List<CommentDto>();
        public IList<RatingDto> Ratings { get; } = new List<RatingDto>();

    }
}
