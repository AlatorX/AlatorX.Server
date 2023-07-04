using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlatorX.Server.Management.Service.DTOs.Websites;
using AlatorX.Server.Management.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlatorX.Server.Management.Controllers
{
    [Authorize]
    public class WebsitesController : BaseController
    {
        private readonly IWebsiteService _websiteService;

        public WebsitesController(IWebsiteService websiteService)
        {
            _websiteService = websiteService;
        }

        [HttpPost]
        public async ValueTask<IActionResult> PostAsync(WebsiteForCreationDto dto)
            => Ok(await _websiteService.AddAsync(dto));
    }
}