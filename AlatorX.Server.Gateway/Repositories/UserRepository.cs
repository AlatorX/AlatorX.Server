using AlatorX.Server.Gateway.Data;
using AlatorX.Server.Gateway.Models;
using Microsoft.EntityFrameworkCore;

namespace AlatorX.Server.Gateway.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext appDbContext;

    public UserRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<bool> IsApiKeyExistsAsync(string apiKey) =>
        await this.appDbContext
            .Set<UserToken>()
            .AnyAsync(userToken => userToken.ApiKey == apiKey);
}
