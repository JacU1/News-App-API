using Microsoft.EntityFrameworkCore;
using News_App_API.Models;
using System.Security.Policy;

namespace News_App_API.Context;

public class NewsAPIContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public NewsAPIContext(DbContextOptions options, IConfiguration configuration)
        : base(options)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var connectionString = Configuration.GetConnectionString("DefaultConnection");
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }

    public DbSet<Article> Articles { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Rating> Ratings { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
}
//dotnet ef database update