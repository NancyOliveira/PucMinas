using Application.Command.Consult;
using AutoMapper;
using Domain.DTO.Consult;
using Domain.Exceptions;
using Infra.Data.Consult;
using MediatR;

namespace Application.Handler.Consult
{
    public class ConsultCommadHandler : IRequestHandler<ConsultCommand, Unit>
    {
        private readonly IConsultWriter _consultWriter;
        private readonly IConsultReader _consultReader;
        private readonly IMapper _mapper;

        public ConsultCommadHandler(IConsultWriter consultWriter, IConsultReader consultReader, IMapper mapper)
        {
            this._consultWriter = consultWriter;
            this._consultReader = consultReader;
            this._mapper = mapper;
        }

        public async Task<Unit> Handle(ConsultCommand request, CancellationToken cancellationToken)
        {
            request.Document = request.Document.Replace(".", "").Replace("-", "");

            if (await this._consultReader.ExistsAync(request.Document, request.DateConsult))
            {
                throw new DuplicateConsultException();
            };

            ConsultDTO consultDTO = this._mapper.Map<ConsultDTO>(request);

            await this._consultWriter.AddAsync(consultDTO);

            return Unit.Value;
        }
    }
}