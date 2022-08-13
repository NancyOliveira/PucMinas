using MediatR;

namespace Application.Command.Consult
{
    public class ConsultCommand : IRequest<Unit>
    {
        public string Document { get; set; }

        public DateTime DateConsult { get; set; }

        public int ServiceID { get; set; }
    }
}