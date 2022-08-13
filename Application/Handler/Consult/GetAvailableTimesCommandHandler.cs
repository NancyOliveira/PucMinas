using Application.Command.Service;
using Domain.Exceptions;
using Infra.Data.Consult;
using Infra.Data.Service;
using MediatR;

namespace Application.Handler.Consult
{
    public class GetAvailableTimesCommandHandler : IRequestHandler<GetAvailableTimesCommand, List<DateTime>>
    {
        private readonly IConsultReader _consultReader;
        private readonly IServiceReader _serviceReader;

        public GetAvailableTimesCommandHandler(IConsultReader consultReader, IServiceReader serviceReader)
        {
            this._consultReader = consultReader;
            this._serviceReader = serviceReader;
        }

        public async Task<List<DateTime>> Handle(GetAvailableTimesCommand request, CancellationToken cancellationToken)
        {
            if (!await this._serviceReader.ExistsAsync(request.ServiceID))
            {
                throw new ServiceNotFoundException();
            }

            List<DateTime> dates = await this._consultReader.GetAvailableTimesAync(request.ServiceID);

            if(dates == null)
            {
                throw new ConsultNotFoundException();
            }

            return dates;
        }
    }
}