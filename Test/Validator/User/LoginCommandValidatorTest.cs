using Application.Command.User;
using Application.Validator.User;
using Domain.Constant.User;
using Newtonsoft.Json;
using Xunit;

namespace Test.Validator.User
{
    public class LoginCommandValidatorTest
    {
        [Fact]
        public void SuccessRequest()
        {
            //Arranje
            LoginCommand loginCommand = new LoginCommand()
            {
                Login = "nancy.sousa",
                Password = "teste$12"
            };
            LoginCommandValidator validation = new LoginCommandValidator();

            //Act
            var resultLogin = validation.Validate(loginCommand);

            //Assert
            Assert.True(resultLogin.Errors.Count == 0);
            Assert.True(resultLogin.IsValid);
        }

        #region Login Test
        [Fact]
        public void UserInvalidRequest()
        {
            //Arranje
            LoginCommand loginCommand = new LoginCommand()
            {
                Login = "nancysousa",
                Password = "teste$12"
            };
            LoginCommandValidator validation = new LoginCommandValidator();

            //Act
            var resultLogin = validation.Validate(loginCommand);

            //Assert
            Assert.True(resultLogin.Errors.Count == 1);
            Assert.False(resultLogin.IsValid);
            Assert.Equal(resultLogin.Errors[0].PropertyName, "Login");
            Assert.Equal(JsonConvert.SerializeObject(UserValidatorConstant.LOGIN_HAS_INVALID_FORMAT), resultLogin.Errors[0].ErrorMessage);
        }

        [Fact]
        public void NotSendLoginRequest()
        {
            //Arranje
            PasswordCommand passwordCommand = new PasswordCommand()
            {
                PasswordNew = "teste$12",
                PasswordOld = "teste$1212"
            };
            PasswordCommandValidator validation = new PasswordCommandValidator();

            //Act
            var resultLogin = validation.Validate(passwordCommand);

            //Assert
            Assert.True(resultLogin.Errors.Count == 1);
            Assert.False(resultLogin.IsValid);
            Assert.Equal(resultLogin.Errors[0].PropertyName, "Login");
            Assert.Equal(JsonConvert.SerializeObject(UserValidatorConstant.LOGIN_MANDATORY), resultLogin.Errors[0].ErrorMessage);
        }
        #endregion

        #region Password
        [Fact]
        public void UserWithNumbersRequest()
        {
            //Arranje
            LoginCommand loginCommand = new LoginCommand()
            {
                Login = "nancy.sousa12",
                Password = "teste$12"
            };
            LoginCommandValidator validation = new LoginCommandValidator();

            //Act
            var resultLogin = validation.Validate(loginCommand);

            //Assert
            Assert.True(resultLogin.Errors.Count == 1);
            Assert.False(resultLogin.IsValid);
            Assert.Equal(resultLogin.Errors[0].PropertyName, "Login");
            Assert.Equal(JsonConvert.SerializeObject(UserValidatorConstant.LOGIN_HAS_INVALID_FORMAT), resultLogin.Errors[0].ErrorMessage);
        }

        [Fact]
        public void PasswordInvalidRequest()
        {
            //Arranje
            LoginCommand loginCommand = new LoginCommand()
            {
                Login = "nancy.sousa",
                Password = "teste"
            };
            LoginCommandValidator validation = new LoginCommandValidator();

            //Act
            var resultLogin = validation.Validate(loginCommand);

            //Assert
            Assert.True(resultLogin.Errors.Count == 1);
            Assert.False(resultLogin.IsValid);
            Assert.Equal(resultLogin.Errors[0].PropertyName, "Password");
            Assert.Equal(JsonConvert.SerializeObject(UserValidatorConstant.PASSWORD_HAS_INVALID), resultLogin.Errors[0].ErrorMessage);
        }

        [Fact]
        public void PasswordWithNumbersRequest()
        {
            //Arranje
            LoginCommand loginCommand = new LoginCommand()
            {
                Login = "nancy.sousa",
                Password = "teste12"
            };
            LoginCommandValidator validation = new LoginCommandValidator();

            //Act
            var resultLogin = validation.Validate(loginCommand);

            //Assert
            Assert.True(resultLogin.Errors.Count == 1);
            Assert.False(resultLogin.IsValid);
            Assert.Equal(resultLogin.Errors[0].PropertyName, "Password");
            Assert.Equal(JsonConvert.SerializeObject(UserValidatorConstant.PASSWORD_HAS_INVALID), resultLogin.Errors[0].ErrorMessage);
        }

        [Fact]
        public void NotSendPasswordRequest()
        {
            //Arranje
            LoginCommand loginCommand = new LoginCommand()
            {
                Login = "nancy.sousa",
            };
            LoginCommandValidator validation = new LoginCommandValidator();

            //Act
            var resultLogin = validation.Validate(loginCommand);

            //Assert
            Assert.True(resultLogin.Errors.Count == 1);
            Assert.False(resultLogin.IsValid);
            Assert.Equal(resultLogin.Errors[0].PropertyName, "Password");
            Assert.Equal(JsonConvert.SerializeObject(UserValidatorConstant.PASSWORD_MANDATORY), resultLogin.Errors[0].ErrorMessage);
        }
        #endregion
    }
}