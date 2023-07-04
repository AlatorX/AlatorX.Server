namespace AlatorX.Server.Gateway.Repositories;

public interface IUserRepository
{
    Task<bool> IsApiKeyExistsAsync(string apiKey);
}
