using AlatorX.Server.Management.Domain.Entities;
using AlatorX.Server.Management.Service.DTOs.AlatorTools;

namespace AlatorX.Server.Management.Service.Interfaces
{
    public interface IAlatorToolService
    {
        ValueTask<IEnumerable<AlatorTool>> RetrieveAllAsync();
        ValueTask<AlatorTool> AddAsync(AlatorToolForCreationDto dto);
        ValueTask<bool> RemoveAsync(long id);
    }
}
