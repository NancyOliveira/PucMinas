using Application.Command.Consult;
using Application.Command.Service;
using Application.Handler.Consult;
using Domain.DTO.Consult;
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
    public class GetConsultCommandHandlerTest
    {
        [Fact]
        public async Task SuccessRequest()
        {
            //Arange
            GetConsultCommand customerCommand = new GetConsultCommand()
            {
                Document = "411.006.148-26"
            };
            List<ConsultDTO> consultDTO = new List<ConsultDTO>();
            consultDTO.Add(new ConsultDTO()
            {
                Document = customerCommand.Document,
                ServiceID = 2,
                DateConsult = DateTime.Now.AddDays(1)
            });

            Mock<IConsultReader> consultReader = new Mock<IConsultReader>();
            consultReader.Setup(x => x.GetAsync(customerCommand.Document)).Returns(Task.FromResult(consultDTO));

            GetConsultCommandHandler handler = new GetConsultCommandHandler(consultReader.Object);

            // Act
            var result = await handler.Handle(customerCommand, new System.Threading.CancellationToken());

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ConsultNotFoundExceptionRequest()
        {
            //Arange
            GetConsultCommand customerCommand = new GetConsultCommand()
            {
                Document = "411.006.148-26"
            };
            List<ConsultDTO> consultDTO = null;

            Mock<IConsultReader> consultReader = new Mock<IConsultReader>();
            consultReader.Setup(x => x.GetAsync(customerCommand.Document)).Returns(Task.FromResult(consultDTO));

            GetConsultCommandHandler handler = new GetConsultCommandHandler(consultReader.Object);

            // Act
            Task act() => handler.Handle(customerCommand, new System.Threading.CancellationToken());

            //Assert
            var exception = await Assert.ThrowsAsync<ConsultNotFoundException>(act);
        }
    }
}
