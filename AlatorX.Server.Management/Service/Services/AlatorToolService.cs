using AlatorX.Server.Management.Data.IRepositories;
using AlatorX.Server.Management.Domain.Entities;
using AlatorX.Server.Management.Service.DTOs.AlatorTools;
using AlatorX.Server.Management.Service.Exceptions;
using AlatorX.Server.Management.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AlatorX.Server.Management.Service.Services
{
    public class AlatorToolService : IAlatorToolService
    {
        private readonly IRepository<AlatorTool> _alatorToolRepository;
        private readonly IWebHostEnvironment _env;

        public AlatorToolService(IRepository<AlatorTool> alatorToolRepository, IWebHostEnvironment env)
        {
            _alatorToolRepository = alatorToolRepository;
            _env = env;
        }

        public async ValueTask<AlatorTool> AddAsync(AlatorToolForCreationDto dto)
        {
            var alatorTool = new AlatorTool()
            {
                Name = dto.Name,
                Description = dto.Description
            };

            string fileName = Path.Combine("images", Guid.NewGuid().ToString("N") + dto.Logo.FileName);
            string path = Path.Combine(_env.WebRootPath, fileName);

            using(var ms = new MemoryStream())
            {
                await dto.Logo.CopyToAsync(ms);
                await File.WriteAllBytesAsync(path, ms.ToArray());
            }

            alatorTool.LogoPath = path;

            return await _alatorToolRepository.InsertAsync(alatorTool);
        }

        public async ValueTask<bool> RemoveAsync(long id)
        {
            var alatorTool = await _alatorToolRepository.SelectByIdAsync(id);
            if (alatorTool == null)
                throw new AlatorException(404, "Alator tool not found");

            return await _alatorToolRepository.DeleteAsync(id);
        }

        public async ValueTask<IEnumerable<AlatorTool>> RetrieveAllAsync()
        {
            return await _alatorToolRepository.SelectAll()
                .ToListAsync();
        }
    }
}
