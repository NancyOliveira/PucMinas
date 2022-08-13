using Domain.DTO.Service;
using MediatR;

namespace Application.Command.Service
{
    public class GetServiceCommand : IRequest<List<ServiceDTO>>
    {
    }
}