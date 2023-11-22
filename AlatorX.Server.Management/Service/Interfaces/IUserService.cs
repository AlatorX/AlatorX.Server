using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlatorX.Server.Management.Domain.Entities;
using AlatorX.Server.Management.Models;
using AlatorX.Server.Management.Service.DTOs.Users;

namespace AlatorX.Server.Management.Service.Interfaces
{
    public interface IUserService 
    {
        ValueTask<UserForResultDto> AddAsync(UserForCreationDto dto);
        ValueTask<UserForResultDto> RetrieveByIdAsync(long userId);
        ValueTask<UserToken> GenerateApiKeyAsync();
        ValueTask<string> GetApiTokenByUserIdAsync(long userId);
        ValueTask<UserForResultDto> GetMeAsync();
        ValueTask<IEnumerable<Website>> GetAllWebsitesAsync();
        ValueTask SendMessageToEmail(Message message);
    }
}