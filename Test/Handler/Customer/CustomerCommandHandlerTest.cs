using Application.Command.Customer;
using Application.Handler.Customer;
using AutoMapper;
using Infra.Data.Customer;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Test.Handler.Customer
{
    public class CustomerCommandHandlerTest
    {
        [Fact]
        public async Task SuccessRequest()
        {
            //Arange
            CustomerCommand customerCommand = new CustomerCommand()
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
            customerReader.Setup(x => x.ExistsAsync(customerCommand.Document)).Returns(Task.FromResult(false));
            Mock<ICustomerWriter> customerWriter = new Mock<ICustomerWriter>();
            Mock<IMapper> mapper = new Mock<IMapper>();
            CustomerCommandHandler handler = new CustomerCommandHandler(customerReader.Object, customerWriter.Object,
                                                                        mapper.Object);

            // Act
            var result = await handler.Handle(customerCommand, new System.Threading.CancellationToken());

            //Assert
            Assert.NotNull(result);
        }
    }
}