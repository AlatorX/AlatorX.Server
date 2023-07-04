using AlatorX.Server.Management.Domain.Entities;
using AlatorX.Server.Management.Service.DTOs.Websites;

namespace AlatorX.Server.Management.Service.Interfaces
{
    public interface IWebsiteService
    {
        ValueTask<Website> AddAsync(WebsiteForCreationDto dto);
    }
}