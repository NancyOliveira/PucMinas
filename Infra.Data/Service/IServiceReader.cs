using Domain.DTO.Service;

namespace Infra.Data.Service
{
    public interface IServiceReader
    {
        Task<List<ServiceDTO>> GetAll();

        Task<bool> ExistsAsync(int serviceID);
    }
}