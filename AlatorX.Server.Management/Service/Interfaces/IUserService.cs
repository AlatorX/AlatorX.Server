using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlatorX.Server.Management.Service.DTOs.Users;

namespace AlatorX.Server.Management.Service.Interfaces
{
    public interface IUserService 
    {
        ValueTask<UserForResultDto> AddAsync(UserForCreationDto dto);
        ValueTask<UserForResultDto> RetrieveByIdAsync(long id);
    }
}