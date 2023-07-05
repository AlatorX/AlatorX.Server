using AlatorX.Server.Gateway.Models;
using Microsoft.EntityFrameworkCore;

namespace AlatorX.Server.Gateway.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<UserToken> UserTokens { get; set; }
    public DbSet<Website> Websites { get; set; }
}
