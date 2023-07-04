using AlatorX.Server.Management.Data.IRepositories;
using AlatorX.Server.Management.Domain.Entities;
using AlatorX.Server.Management.Service.DTOs.Websites;
using AlatorX.Server.Management.Service.Interfaces;

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
                Name = dto.Name
            });

            return website;
        }
    }
}