using Application.Command.Service;
using Application.Handler.Service;
using Domain.DTO.Service;
using Domain.Exceptions;
using Infra.Data.Service;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Test.Handler.Service
{
    public class ServiceCommadHandlerTest
    {
        [Fact]
        public async Task SuccessRequest()
        {
            //Arange
            GetServiceCommand passwordCommand = new GetServiceCommand()
            {
            };
            List<ServiceDTO> serviceDTOs = new List<ServiceDTO>();
            Mock<IServiceReader> serviceReader = new Mock<IServiceReader>();
            serviceReader.Setup(x => x.GetAll()).Returns(Task.FromResult(serviceDTOs));
            ServiceCommadHandler handler = new ServiceCommadHandler(serviceReader.Object);

            // Act
            var result = await handler.Handle(passwordCommand, new System.Threading.CancellationToken());

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ServiceNotFoundExceptionRequest()
        {
            //Arange
            GetServiceCommand passwordCommand = new GetServiceCommand()
            {
            };
            List<ServiceDTO> serviceDTOs = null;
            Mock<IServiceReader> serviceReader = new Mock<IServiceReader>();
            serviceReader.Setup(x => x.GetAll()).Returns(Task.FromResult(serviceDTOs));
            ServiceCommadHandler handler = new ServiceCommadHandler(serviceReader.Object);

            // Act
            Task act() => handler.Handle(passwordCommand, new System.Threading.CancellationToken());

            //Assert
            var exception = await Assert.ThrowsAsync<ServiceNotFoundException>(act);
        }
    }
}