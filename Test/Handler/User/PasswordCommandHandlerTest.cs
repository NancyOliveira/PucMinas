using Application.Command.User;
using Application.Handler.User;
using Domain.Exceptions;
using Infra.Data.User;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Test.Handler.User
{
    public class PasswordCommandHandlerTest
    {
        [Fact]
        public async Task SuccessRequest()
        {
            //Arange
            PasswordCommand passwordCommand = new PasswordCommand()
            {
                Login = "nancy.sousa",
                PasswordNew = "teste$12",
                PasswordOld = "teste$1212"
            };

            Mock<IUserWriter> userRWriter = new Mock<IUserWriter>();
            Mock<IUserReader> userReader = new Mock<IUserReader>();
            userReader.Setup(x => x.ExistsAsync(passwordCommand.Login, passwordCommand.PasswordOld)).Returns(Task.FromResult(true));
            PasswordCommandHandler handler = new PasswordCommandHandler(userRWriter.Object, userReader.Object);

            // Act
            var result = await handler.Handle(passwordCommand, new System.Threading.CancellationToken());

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UnauthorizedExceptionRequest()
        {
            //Arange
            PasswordCommand passwordCommand = new PasswordCommand()
            {
                Login = "nancy sousa",
                PasswordNew = "teste$12",
                PasswordOld = "teste$1212"
            };

            Mock<IUserWriter> userRWriter = new Mock<IUserWriter>();
            Mock<IUserReader> userReader = new Mock<IUserReader>();
            userReader.Setup(x => x.ExistsAsync(passwordCommand.Login, passwordCommand.PasswordOld)).Returns(Task.FromResult(false));
            PasswordCommandHandler handler = new PasswordCommandHandler(userRWriter.Object, userReader.Object);

            // Act
            Task act() => handler.Handle(passwordCommand, new System.Threading.CancellationToken());

            //Assert
            var exception = await Assert.ThrowsAsync<UnauthorizedException>(act);
        }

        [Fact]
        public async Task InvalidPasswordExceptionRequest()
        {
            //Arange
            PasswordCommand passwordCommand = new PasswordCommand()
            {
                Login = "nancy.sousa",
                PasswordNew = "teste$12",
                PasswordOld = "teste$12"
            };

            Mock<IUserWriter> userRWriter = new Mock<IUserWriter>();
            Mock<IUserReader> userReader = new Mock<IUserReader>();
            userReader.Setup(x => x.ExistsAsync(passwordCommand.Login, passwordCommand.PasswordOld)).Returns(Task.FromResult(true));
            PasswordCommandHandler handler = new PasswordCommandHandler(userRWriter.Object, userReader.Object);

            // Act
            Task act() => handler.Handle(passwordCommand, new System.Threading.CancellationToken());

            //Assert
            var exception = await Assert.ThrowsAsync<InvalidPasswordException>(act);
        }
    }
}
