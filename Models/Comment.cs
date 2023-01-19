using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace News_App_API.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        [Required]
        [Display(Name = "ArticleId")]
        public virtual int? ArticleId { get; set; }
        [ForeignKey("ArticleId")]
        public Article? Article { get; set; }
        [Required]
        [Display(Name = "UserId")]
        public virtual int? UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
