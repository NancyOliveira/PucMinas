using Application.Command.Customer;
using AutoMapper;
using Domain.DTO.Customer;
using Domain.Exceptions;
using Infra.Data.Customer;
using MediatR;

namespace Application.Handler.Customer
{
    public class CustomerCommandHandler : IRequestHandler<CustomerCommand>
    {
        private readonly ICustomerReader _customerReader;
        private readonly ICustomerWriter _customerWriter;
        private readonly IMapper _mapper;

        public CustomerCommandHandler(ICustomerReader customerReader, ICustomerWriter customerWriter, IMapper mapper)
        {
            this._customerReader = customerReader;
            this._customerWriter = customerWriter;
            this._mapper = mapper;
        }

        public async Task<Unit> Handle(CustomerCommand request, CancellationToken cancellationToken)
        {
            request.Document = request.Document.Replace(".", "").Replace("-", "");

            if (await this._customerReader.ExistsAsync(request.Document))
            {
                throw new DuplicateDocumentException();
            }

            CustomerDTO customerDTO = this._mapper.Map<CustomerDTO>(request);

            await this._customerWriter.AddAsync(customerDTO);

            return Unit.Value;
        }
    }
}
