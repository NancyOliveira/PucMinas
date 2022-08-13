using Application.Command.Customer;
using Domain.DTO.Customer;
using Domain.Exceptions;
using Infra.Data.Customer;
using MediatR;

namespace Application.Handler.Customer
{
    public class GetDocumentCommandHandler : IRequestHandler<GetDocumentCommand, CustomerDTO>
    {
        private readonly ICustomerReader _customerReader;

        public GetDocumentCommandHandler(ICustomerReader customerReader)
        {
            this._customerReader = customerReader;
        }

        public async Task<CustomerDTO> Handle(GetDocumentCommand request, CancellationToken cancellationToken)
        {
            CustomerDTO customerDTO = await this._customerReader.GetDocumentAsync(request.Document);

            if(customerDTO == null)
            {
                throw new DocumentNotFoundException();
            }

            return customerDTO;
        }
    }
}
