using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlatorX.Server.Management.Service.DTOs.Users;
using AlatorX.Server.Management.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlatorX.Server.Management.Controllers
{
    [Authorize]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{Id}")]
        public async ValueTask<IActionResult> GetAsync([FromRoute(Name = "Id")] long id)
            => Ok(await _userService.RetrieveByIdAsync(id));

        [HttpPost, AllowAnonymous]
        public async ValueTask<IActionResult> PostAsync([FromBody] UserForCreationDto dto)
            => Ok(await _userService.AddAsync(dto));
    }
}