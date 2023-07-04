using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlatorX.Server.Management.Data.IRepositories;
using AlatorX.Server.Management.Domain.Entities;
using AlatorX.Server.Management.Service.DTOs.Websites;
using AlatorX.Server.Management.Service.Helpers;
using AlatorX.Server.Management.Service.Interfaces;

namespace AlatorX.Server.Management.Service.Services
{
    public class WebsiteService : IWebsiteService
    {
        private readonly IRepository<Website> _websiteRepository;
        private readonly IRepository<WebsiteSetting> _websiteSettingRepository;
        private readonly IRepository<UserWebsite> _userWebsiteRepository;

        public WebsiteService(IRepository<Website> websiteRepository, IRepository<WebsiteSetting> websiteSettingRepository, 
            IRepository<UserWebsite> userWebsiteRepository)
        {
            _websiteRepository = websiteRepository;
            _websiteSettingRepository = websiteSettingRepository;
            _userWebsiteRepository = userWebsiteRepository;
        }


        public async ValueTask<WebsiteSetting> AddAsync(WebsiteForCreationDto dto)
        {
            var website = await _websiteRepository.InsertAsync(new Website
            {
                Name = dto.Name
            });

            var websiteSetting = await _websiteSettingRepository.InsertAsync(new WebsiteSetting
            {
                WebsiteId = website.Id,
                ConfigString = dto.ConfigString
            });

            var userWebsite = await _userWebsiteRepository.InsertAsync(new UserWebsite
            {
                UserId = HttpContextHelper.UserId ?? throw new UnauthorizedAccessException(),
                WebsiteSettingId = websiteSetting.Id
            });

            return websiteSetting;
        }
    }
}