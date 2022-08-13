using MediatR;

namespace Application.Command.Customer
{
    public class CustomerCommand : IRequest<Unit>
    {
        public string Name { get; set; }

        public string Document { get; set; }

        public DateTime Birthdate { get; set; }

        public string Adress { get; set; }

        public string NumberAdress { get; set; }

        public int CEP { get; set; }

        public int DDD { get; set; }

        public int Phone { get; set; }
    }
}