using Domain.DTO.Customer;
using MediatR;

namespace Application.Command.Customer
{
    public class GetDocumentCommand : IRequest<CustomerDTO>
    {
        public string Document { get; set; }
    }
}