using AlatorX.Server.Gateway.Models;

namespace AlatorX.Server.Gateway.Repositories;

public interface IWebsiteRepository
{
    Task<Website> SelectWebsiteAsync(long userId, long siteId);
    IQueryable<Website> SelectAllWebsites();
}
