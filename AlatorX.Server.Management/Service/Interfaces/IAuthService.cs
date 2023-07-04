using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlatorX.Server.Management.Service.DTOs.Users;

namespace AlatorX.Server.Management.Service.Interfaces
{
    public interface IAuthService
    {
        ValueTask<LoginForResultDto> AuthenticateAsync(LoginDto dto);
    }
}