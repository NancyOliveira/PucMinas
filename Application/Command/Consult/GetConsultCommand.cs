using Domain.DTO.Consult;
using MediatR;

namespace Application.Command.Consult
{
    public class GetConsultCommand : IRequest<List<ConsultDTO>>
    {
        public string Document { get; set; }
    }
}