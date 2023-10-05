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

    public DbSet<ArticleDto> Articles { get; set; } = null!;
    public DbSet<UserDto> Users { get; set; } = null!;
    public DbSet<RatingDto> Ratings { get; set; } = null!;
    public DbSet<CommentDto> Comments { get; set; } = null!;
    public DbSet<UserAuthDto> UsersAuth { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var rootUserGuid = Guid.NewGuid();

        modelBuilder.Entity<UserAuthDto>().HasData(new UserAuthDto
        {
            Id = rootUserGuid,
            Email = "test@gmail.com",
            Password = "root",
        });

        modelBuilder.Entity<UserDto>().HasData(new UserDto
        {
            Id = rootUserGuid,
            Name = "AdminName",
            LastName = "AdminLastName",
            Email = "test@gmail.com",
            UserTag = "AdminTag"
        });
    }
}

//dotnet ef database update