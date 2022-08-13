using Domain.DTO.Consult;

namespace Infra.Data.Consult
{
    public interface IConsultWriter
    {
        Task AddAsync(ConsultDTO consultDTO);
    }
}