using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace News_App_API.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public string? Content { get; set; }
        [Required]
        [Display(Name = "UserId")]
        public virtual int? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
        public IList<Comment> Comments { get; } = new List<Comment>();
        public IList<Rating> Ratings { get; } = new List<Rating>();
    }
}
