using Application.Command.Consult;
using Application.Handler.Consult;
using AutoMapper;
using Infra.Data.Consult;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Test.Handler.Consult
{
    public class ConsultCommadHandlerTest
    {
        [Fact]
        public async Task SuccessRequest()
        {
            //Arange
            ConsultCommand customerCommand = new ConsultCommand()
            {
                Document = "411.006.148-26",
                DateConsult = DateTime.Now,
                ServiceID = 1
            };

            Mock<IConsultWriter> consultWriter = new Mock<IConsultWriter>();
            Mock<IConsultReader> consultReader = new Mock<IConsultReader>();
            Mock<IMapper> mapper = new Mock<IMapper>();
            consultReader.Setup(x => x.ExistsAync(customerCommand.Document, customerCommand.DateConsult)).Returns(Task.FromResult(false));
            ConsultCommadHandler handler = new ConsultCommadHandler(consultWriter.Object, consultReader.Object, mapper.Object);

            // Act
            var result = await handler.Handle(customerCommand, new System.Threading.CancellationToken());

            //Assert
            Assert.NotNull(result);
        }
    }
}