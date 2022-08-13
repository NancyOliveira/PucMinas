using Application.Command.Service;
using Application.Handler.Consult;
using Domain.Exceptions;
using Infra.Data.Consult;
using Infra.Data.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Test.Handler.Consult
{
    public class GetAvailableTimesCommandHandlerTest
    {
        [Fact]
        public async Task SuccessRequest()
        {
            //Arange
            GetAvailableTimesCommand customerCommand = new GetAvailableTimesCommand()
            {
                ServiceID = 1
            };
            List<DateTime> dates = new List<DateTime>();

            Mock<IConsultReader> consultReader = new Mock<IConsultReader>();
            consultReader.Setup(x => x.GetAvailableTimesAync(customerCommand.ServiceID)).Returns(Task.FromResult(dates));
            Mock<IServiceReader> serviceReader = new Mock<IServiceReader>();
            serviceReader.Setup(x => x.ExistsAsync(customerCommand.ServiceID)).Returns(Task.FromResult(true));

            GetAvailableTimesCommandHandler handler = new GetAvailableTimesCommandHandler(consultReader.Object,
                                                                                          serviceReader.Object);

            // Act
            var result = await handler.Handle(customerCommand, new System.Threading.CancellationToken());

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ServiceNotFoundExceptionRequest()
        {
            //Arange
            GetAvailableTimesCommand customerCommand = new GetAvailableTimesCommand()
            {
                ServiceID = 1
            };
            List<DateTime> dates = new List<DateTime>();

            Mock<IConsultReader> consultReader = new Mock<IConsultReader>();
            consultReader.Setup(x => x.GetAvailableTimesAync(customerCommand.ServiceID)).Returns(Task.FromResult(dates));
            Mock<IServiceReader> serviceReader = new Mock<IServiceReader>();
            serviceReader.Setup(x => x.ExistsAsync(customerCommand.ServiceID)).Returns(Task.FromResult(false));

            GetAvailableTimesCommandHandler handler = new GetAvailableTimesCommandHandler(consultReader.Object,
                                                                                          serviceReader.Object);

            // Act
            Task act() => handler.Handle(customerCommand, new System.Threading.CancellationToken());

            //Assert
            var exception = await Assert.ThrowsAsync<ServiceNotFoundException>(act);
        }

        [Fact]
        public async Task ConsultNotFoundExceptionRequest()
        {
            //Arange
            GetAvailableTimesCommand customerCommand = new GetAvailableTimesCommand()
            {
                ServiceID = 1
            };
            List<DateTime> dates = null;

            Mock<IConsultReader> consultReader = new Mock<IConsultReader>();
            consultReader.Setup(x => x.GetAvailableTimesAync(customerCommand.ServiceID)).Returns(Task.FromResult(dates));
            Mock<IServiceReader> serviceReader = new Mock<IServiceReader>();
            serviceReader.Setup(x => x.ExistsAsync(customerCommand.ServiceID)).Returns(Task.FromResult(true));

            GetAvailableTimesCommandHandler handler = new GetAvailableTimesCommandHandler(consultReader.Object,
                                                                                          serviceReader.Object);

            // Act
            Task act() => handler.Handle(customerCommand, new System.Threading.CancellationToken());

            //Assert
            var exception = await Assert.ThrowsAsync<ConsultNotFoundException>(act);
        }
    }
}
