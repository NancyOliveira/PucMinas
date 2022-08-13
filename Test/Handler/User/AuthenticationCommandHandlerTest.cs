using Application.Command.User;
using Application.Handler.User;
using Domain.Exceptions;
using Infra.ACL.Jwt;
using Infra.Data.User;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Test.Handler.User
{
    public class AuthenticationCommandHandlerTest
    {
        [Fact]
        public async Task SuccessRequest()
        {
            //Arange
            LoginCommand loginCommand = new LoginCommand()
            {
                Login = "nancy.sousa",
                Password = "teste$12"
            };

            DateTime date = DateTime.UtcNow.AddSeconds(86400);

            Mock<IJwt> jwt = new Mock<IJwt>();
            jwt.Setup(x => x.CreateToken(loginCommand.Login, date)).Returns(Guid.NewGuid().ToString());

            Mock<IUserReader> userReader = new Mock<IUserReader>();
            userReader.Setup(x => x.ExistsAsync(loginCommand.Login, loginCommand.Password)).Returns(Task.FromResult(true));

            Mock <IConfiguration> config = new Mock<IConfiguration>();
            config.Setup(x => x["Jwt:Seconds"]).Returns("86400");
            config.Setup(x => x["Jwt:Secret"]).Returns("MJTdkkKU5KxZ6NbgldRdIgq5jlTp8za8");

            AuthenticationCommandHandler handler = new AuthenticationCommandHandler(jwt.Object, userReader.Object, 
                                                                                    config.Object);
            // Act
            var result = await handler.Handle(loginCommand, new System.Threading.CancellationToken());

            //Assert
            Assert.Equal(null, result.Token);
            Assert.NotNull(result.ExpirationDate);
            Assert.NotEqual(result.ExpirationDate, DateTime.MinValue);
        }

        [Fact]
        public async Task UnauthorizedExceptionRequest()
        {
            //Arange
            LoginCommand loginCommand = new LoginCommand()
            {
                Login = "nancy.sousa",
                Password = "teste$12"
            };

            DateTime date = DateTime.UtcNow.AddSeconds(86400);

            Mock<IJwt> jwt = new Mock<IJwt>();
            jwt.Setup(x => x.CreateToken(loginCommand.Login, date)).Returns(Guid.NewGuid().ToString());

            Mock<IUserReader> userReader = new Mock<IUserReader>();
            userReader.Setup(x => x.ExistsAsync(loginCommand.Login, loginCommand.Password)).Returns(Task.FromResult(false));

            Mock <IConfiguration> config = new Mock<IConfiguration>();
            config.Setup(x => x["Jwt:Seconds"]).Returns("86400");
            config.Setup(x => x["Jwt:Secret"]).Returns("MJTdkkKU5KxZ6NbgldRdIgq5jlTp8za8");

            AuthenticationCommandHandler handler = new AuthenticationCommandHandler(jwt.Object, userReader.Object, 
                                                                                    config.Object);

            // Act
            Task act() => handler.Handle(loginCommand, new System.Threading.CancellationToken());

            //Assert
            var exception = await Assert.ThrowsAsync<UnauthorizedException>(act);
        }
    }
}