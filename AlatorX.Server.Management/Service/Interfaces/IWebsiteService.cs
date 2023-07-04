using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlatorX.Server.Management.Domain.Entities;
using AlatorX.Server.Management.Service.DTOs.Websites;

namespace AlatorX.Server.Management.Service.Interfaces
{
    public interface IWebsiteService
    {
        ValueTask<WebsiteSetting> AddAsync(WebsiteForCreationDto dto);
    }
}