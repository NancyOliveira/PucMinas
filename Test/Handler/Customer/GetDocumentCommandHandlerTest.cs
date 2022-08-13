using Application.Command.Customer;
using Application.Handler.Customer;
using Domain.DTO.Customer;
using Domain.Exceptions;
using Infra.Data.Customer;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Test.Handler.Customer
{
    public class GetDocumentCommandHandlerTest
    {
        [Fact]
        public async Task SuccessRequest()
        {
            //Arange
            GetDocumentCommand getCustomerCommand = new GetDocumentCommand()
            {
                Document = "411.006.148-26",
            };
            CustomerDTO customerDTO = new CustomerDTO()
            {
                Name = "Paulo Fernandes",
                Document = "411.006.148-26",
                Adress = "Rua dos Alfeneiros",
                NumberAdress = "4",
                CEP = 05241294,
                Birthdate = DateTime.Now.AddYears(-27),
                DDD = 11,
                Phone = 973184319
            };

            Mock<ICustomerReader> customerReader = new Mock<ICustomerReader>();
            customerReader.Setup(x => x.GetDocumentAsync(getCustomerCommand.Document)).Returns(Task.FromResult(customerDTO));
            GetDocumentCommandHandler handler = new GetDocumentCommandHandler(customerReader.Object);

            // Act
            var result = await handler.Handle(getCustomerCommand, new System.Threading.CancellationToken());

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.Name, customerDTO.Name);
            Assert.Equal(result.Document, customerDTO.Document);
            Assert.Equal(result.Adress, customerDTO.Adress);
            Assert.Equal(result.NumberAdress, customerDTO.NumberAdress);
            Assert.Equal(result.CEP, customerDTO.CEP);
            Assert.Equal(result.Birthdate, customerDTO.Birthdate);
            Assert.Equal(result.DDD, customerDTO.DDD);
            Assert.Equal(result.Phone, customerDTO.Phone);
        }

        [Fact]
        public async Task DocumentNotFoundExceptionRequest()
        {
            //Arange
            GetDocumentCommand getCustomerCommand = new GetDocumentCommand()
            {
                Document = "411.006.148-26",
            };
            CustomerDTO customerDTO = null;

            Mock<ICustomerReader> customerReader = new Mock<ICustomerReader>();
            customerReader.Setup(x => x.GetDocumentAsync(getCustomerCommand.Document)).Returns(Task.FromResult(customerDTO));
            GetDocumentCommandHandler handler = new GetDocumentCommandHandler(customerReader.Object);

            // Act
            Task act() => handler.Handle(getCustomerCommand, new System.Threading.CancellationToken());

            //Assert
            var exception = await Assert.ThrowsAsync<DocumentNotFoundException>(act);
        }
    }
}
