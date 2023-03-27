using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace News_App_API.Models
{
    public class ArticleDto
    {
        [Key]
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public string? Content { get; set; }
        [Required]
        [Display(Name = "UserId")]
        public virtual Guid? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserDto? User { get; set; }
        public IList<CommentDto> Comments { get; } = new List<CommentDto>();
        public IList<RatingDto> Ratings { get; } = new List<RatingDto>();
    }
}
