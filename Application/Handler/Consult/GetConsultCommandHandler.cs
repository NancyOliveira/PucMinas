using Application.Command.Consult;
using Domain.DTO.Consult;
using Domain.Exceptions;
using Infra.Data.Consult;
using MediatR;

namespace Application.Handler.Consult
{
    public class GetConsultCommandHandler : IRequestHandler<GetConsultCommand, List<ConsultDTO>>
    {
        private readonly IConsultReader _consultReader;

        public GetConsultCommandHandler(IConsultReader consultReader)
        {
            this._consultReader = consultReader;
        }

        public async Task<List<ConsultDTO>> Handle(GetConsultCommand request, CancellationToken cancellationToken)
        {
            List<ConsultDTO> consultDTO = await this._consultReader.GetAsync(request.Document);

            if(consultDTO?.Count == 0 || consultDTO == null)
            {
                throw new ConsultNotFoundException();
            }

            return consultDTO;
        }
    }
}