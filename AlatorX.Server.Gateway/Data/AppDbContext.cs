using AlatorX.Server.Gateway.Models;
using Microsoft.EntityFrameworkCore;

namespace AlatorX.Server.Gateway.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserToken> UserTokens { get; set; }
}
