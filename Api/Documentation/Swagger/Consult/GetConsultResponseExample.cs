using Domain.DTO.Consult;
using Swashbuckle.AspNetCore.Filters;

namespace Api.Documentation.Swagger.Consult
{
    public class GetConsultResponseExample : IExamplesProvider<List<ConsultDTO>>
    {
        public List<ConsultDTO> GetExamples()
        {
            List<ConsultDTO> consultDTOs = new List<ConsultDTO>();

            consultDTOs.Add(new ConsultDTO()
            {
                Document = "840.614.608-70",
                DateConsult = DateTime.Now.AddDays(2).AddHours(12),
                ServiceID = 1
            });
            consultDTOs.Add(new ConsultDTO()
            {
                Document = "555.873.298-95",
                DateConsult = DateTime.Now.AddDays(4).AddHours(12),
                ServiceID = 1
            });

            return consultDTOs;
        }
    }
}
