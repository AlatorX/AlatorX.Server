using AlatorX.Server.Management.Service.DTOs.AlatorTools;
using AlatorX.Server.Management.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AlatorX.Server.Management.Controllers
{
    public class AlatorToolsController : BaseController
    {
        private readonly IAlatorToolService _alatorToolService;

        public AlatorToolsController(IAlatorToolService alatorToolService)
        {
            _alatorToolService=alatorToolService;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAllAsync()
            => Ok(await _alatorToolService.RetrieveAllAsync());

        [HttpDelete]
        public async ValueTask<IActionResult> DeleteAsync([FromRoute(Name = "Id")] long id)
            => Ok(await _alatorToolService.RemoveAsync(id));

        [HttpPost]
        public async ValueTask<IActionResult> PostAsync([FromForm] AlatorToolForCreationDto dto)
            => Ok(await _alatorToolService.AddAsync(dto));
    }
}
