using MediatR;

namespace Application.Command.Service
{
    public class GetAvailableTimesCommand : IRequest<List<DateTime>>
    {
        public int ServiceID { get; set; }
    }
}