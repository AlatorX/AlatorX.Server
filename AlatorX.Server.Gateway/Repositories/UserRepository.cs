using AlatorX.Server.Gateway.Data;

namespace AlatorX.Server.Gateway.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext appDbContext;

    public UserRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public Task<bool> IsApiKeyExistsAsync(string apiKey)
    {
        throw new NotImplementedException();
    }
}
