using Domain.DTO.Consult;

namespace Infra.Data.Consult
{
    public interface IConsultReader
    {
        Task<bool> ExistsAync(string document, DateTime dateConsult);

        Task<List<ConsultDTO>> GetAsync(string document);

        Task<List<DateTime>> GetAvailableTimesAync(int serviceID);
    }
}