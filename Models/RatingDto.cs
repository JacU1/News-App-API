using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace News_App_API.Models
{
    public class RatingDto
    {
        [Key]
        public Guid Id { get; set; }
        public int Value { get; set; }
        [Required]
        [Display(Name = "ArticleId")]
        public virtual Guid? ArticleId { get; set; }
        [ForeignKey("ArticleId")]
        public ArticleDto? Article { get; set; }
        [Required]
        [Display(Name = "UserId")]
        public virtual Guid? UserId { get; set; }
        [ForeignKey("UserId")]
        public UserDto? User { get; set; }
    }
}
