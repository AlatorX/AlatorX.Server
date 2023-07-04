using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlatorX.Server.Management.Service.DTOs.Users;
using AlatorX.Server.Management.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AlatorX.Server.Management.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async ValueTask<IActionResult> LoginAsync([FromBody] LoginDto dto)
            => Ok(await _authService.AuthenticateAsync(dto));
    }
}