using AlatorX.Server.Management.Data.IRepositories;
using AlatorX.Server.Management.Domain.Entities;
using AlatorX.Server.Management.Service.DTOs.Websites;
using AlatorX.Server.Management.Service.Exceptions;
using AlatorX.Server.Management.Service.Helpers;
using AlatorX.Server.Management.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AlatorX.Server.Management.Service.Services
{
    public class WebsiteService : IWebsiteService
    {
        private readonly IRepository<Website> _websiteRepository;

        public WebsiteService(IRepository<Website> websiteRepository)
        {
            _websiteRepository = websiteRepository;
        }

        public async ValueTask<Website> AddAsync(WebsiteForCreationDto dto)
        {
            var website = await _websiteRepository.InsertAsync(new Website
            {
                Name = dto.Name,
                UserId = HttpContextHelper.UserId ?? throw new UnauthorizedAccessException(),
                ConfigString = dto.ConfigString
            });

            return website;
        }

        public async ValueTask<Website> ModifyAsync(long id, WebsiteForCreationDto dto)
        {
            var website = await _websiteRepository.SelectByIdAsync(id);
            if(website == null)
                throw new AlatorException(404, "Website not found");

            website.ConfigString = dto.ConfigString;
            website.Name = dto.Name;
            website.UpdatedAt = DateTime.UtcNow;

            await _websiteRepository.SaveChangesAsync();

            return website;
        }

        public async ValueTask<IEnumerable<Website>> RetrieveAllAsync()
        {
            return await _websiteRepository.SelectAll()
                .ToListAsync();
        }
    }
}