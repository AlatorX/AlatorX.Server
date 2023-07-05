using AlatorX.Server.Gateway.Data;
using AlatorX.Server.Gateway.Models;
using Microsoft.EntityFrameworkCore;

namespace AlatorX.Server.Gateway.Repositories;

public class WebsiteRepository : IWebsiteRepository
{
    private readonly AppDbContext appDbContext;

    public WebsiteRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public IQueryable<Website> SelectAllWebsites() => this.appDbContext.Websites;

    public async Task<Website> SelectWebsiteAsync(
        long userId,
        long siteId) =>
        await this.appDbContext
            .Websites
            .FirstOrDefaultAsync(website =>
                website.UserId == userId && website.Id == siteId);
}
