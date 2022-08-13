using Domain.DTO.Service;
using Swashbuckle.AspNetCore.Filters;

namespace Api.Documentation.Swagger.Service
{
    public class ServiceResponseExemple : IExamplesProvider<List<ServiceDTO>>
    {
        public List<ServiceDTO> GetExamples()
        {
            List<ServiceDTO> consultDTOs = new List<ServiceDTO>();

            consultDTOs.Add(new ServiceDTO()
            {
                ID = 1,
                Name = "Avaliação dentária",
                Active = true,
                Date = DateTime.Now.AddDays(-1)
            });
            consultDTOs.Add(new ServiceDTO()
            {
                ID = 2,
                Name = "Manutenção do aparelho dentário",
                Active = true,
                Date = DateTime.Now.AddDays(-1)
            });
            consultDTOs.Add(new ServiceDTO()
            {
                ID = 3,
                Name = "Remoção de cárie",
                Active = true,
                Date = DateTime.Now.AddDays(-1)
            });

            return consultDTOs;
        }
    }
}