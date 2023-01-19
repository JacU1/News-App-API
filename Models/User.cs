using Microsoft.Extensions.Hosting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace News_App_API.Models
{
    public class User
    {
        public int Id { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [PasswordPropertyText]
        public string? Password { get; set; }
        public string? UserTag { get; set; }
        public IList<Article> Articles { get; } = new List<Article>();
        public IList<Comment> Comments { get; } = new List<Comment>();
        public IList<Rating> Ratings { get; } = new List<Rating>();

    }
}
