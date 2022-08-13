using Application.Command.Service;
using Domain.DTO.Service;
using Domain.Exceptions;
using Infra.Data.Service;
using MediatR;

namespace Application.Handler.Service
{
    public class ServiceCommadHandler : IRequestHandler<GetServiceCommand, List<ServiceDTO>>
    {
        private readonly IServiceReader _serviceWriter;

        public ServiceCommadHandler(IServiceReader serviceWriter)
        {
            this._serviceWriter = serviceWriter;
        }

        public async Task<List<ServiceDTO>> Handle(GetServiceCommand request, CancellationToken cancellationToken)
        {
            List<ServiceDTO> serviceDTOs = await this._serviceWriter.GetAll();

            if(serviceDTOs == null)
            {
                throw new ServiceNotFoundException();
            }

            return serviceDTOs;
        }
    }
}
