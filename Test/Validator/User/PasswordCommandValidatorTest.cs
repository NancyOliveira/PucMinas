using Application.Command.User;
using Application.Validator.User;
using Domain.Constant.User;
using Newtonsoft.Json;
using Xunit;

namespace Test.Validator.User
{
    public class PasswordCommandValidatorTest
    {
        [Fact]
        public void SuccessRequest()
        {
            //Arranje
            PasswordCommand passwordCommand = new PasswordCommand()
            {
                Login = "nancy.sousa",
                PasswordNew = "teste$12",
                PasswordOld = "teste$1212"
            };
            PasswordCommandValidator validation = new PasswordCommandValidator();

            //Act
            var resultLogin = validation.Validate(passwordCommand);

            //Assert
            Assert.True(resultLogin.Errors.Count == 0);
            Assert.True(resultLogin.IsValid);
        }

        #region Login Test
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

        [Fact]
        public void UserInvalidRequest()
        {
            //Arranje
            PasswordCommand passwordCommand = new PasswordCommand()
            {
                Login = "nancysousa",
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
            Assert.Equal(JsonConvert.SerializeObject(UserValidatorConstant.LOGIN_HAS_INVALID_FORMAT), resultLogin.Errors[0].ErrorMessage);
        }

        [Fact]
        public void UserWithNumbersRequest()
        {
            //Arranje
            PasswordCommand passwordCommand = new PasswordCommand()
            {
                Login = "nancy.sousa12",
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
            Assert.Equal(JsonConvert.SerializeObject(UserValidatorConstant.LOGIN_HAS_INVALID_FORMAT), resultLogin.Errors[0].ErrorMessage);
        }
        #endregion

        #region Password Old Test
        [Fact]
        public void PasswordOldInvalidRequest()
        {
            //Arranje
            PasswordCommand passwordCommand = new PasswordCommand()
            {
                Login = "nancy.sousa",
                PasswordNew = "teste$12",
                PasswordOld = "teste"
            };
            PasswordCommandValidator validation = new PasswordCommandValidator();

            //Act
            var resultLogin = validation.Validate(passwordCommand);

            //Assert
            Assert.True(resultLogin.Errors.Count == 1);
            Assert.False(resultLogin.IsValid);
            Assert.Equal(resultLogin.Errors[0].PropertyName, "PasswordOld");
            Assert.Equal(JsonConvert.SerializeObject(UserValidatorConstant.PASSWORD_OLD_HAS_INVALID), resultLogin.Errors[0].ErrorMessage);
        }

        [Fact]
        public void PasswordOldWithNumbersRequest()
        {
            //Arranje
            PasswordCommand passwordCommand = new PasswordCommand()
            {
                Login = "nancy.sousa",
                PasswordNew = "teste$12",
                PasswordOld = "teste12"
            };
            PasswordCommandValidator validation = new PasswordCommandValidator();

            //Act
            var resultLogin = validation.Validate(passwordCommand);

            //Assert
            Assert.True(resultLogin.Errors.Count == 1);
            Assert.False(resultLogin.IsValid);
            Assert.Equal(resultLogin.Errors[0].PropertyName, "PasswordOld");
            Assert.Equal(JsonConvert.SerializeObject(UserValidatorConstant.PASSWORD_OLD_HAS_INVALID), resultLogin.Errors[0].ErrorMessage);
        }

        [Fact]
        public void NotSendPasswordOldRequest()
        {
            //Arranje
            PasswordCommand passwordCommand = new PasswordCommand()
            {
                Login = "nancy.sousa",
                PasswordNew = "teste$12"
            };
            PasswordCommandValidator validation = new PasswordCommandValidator();

            //Act
            var resultLogin = validation.Validate(passwordCommand);

            //Assert
            Assert.True(resultLogin.Errors.Count == 1);
            Assert.False(resultLogin.IsValid);
            Assert.Equal(resultLogin.Errors[0].PropertyName, "PasswordOld");
            Assert.Equal(JsonConvert.SerializeObject(UserValidatorConstant.PASSWORD_OLD_MANDATORY), resultLogin.Errors[0].ErrorMessage);
        }
        #endregion

        #region Password Old Test
        [Fact]
        public void PasswordNewInvalidRequest()
        {
            //Arranje
            PasswordCommand passwordCommand = new PasswordCommand()
            {
                Login = "nancy.sousa",
                PasswordOld = "teste$12",
                PasswordNew = "teste"
            };
            PasswordCommandValidator validation = new PasswordCommandValidator();

            //Act
            var resultLogin = validation.Validate(passwordCommand);

            //Assert
            Assert.True(resultLogin.Errors.Count == 1);
            Assert.False(resultLogin.IsValid);
            Assert.Equal(resultLogin.Errors[0].PropertyName, "PasswordNew");
            Assert.Equal(JsonConvert.SerializeObject(UserValidatorConstant.PASSWORD_NEW_HAS_INVALID), resultLogin.Errors[0].ErrorMessage);
        }

        [Fact]
        public void PasswordNewWithNumbersRequest()
        {
            //Arranje
            PasswordCommand passwordCommand = new PasswordCommand()
            {
                Login = "nancy.sousa",
                PasswordOld = "teste$12",
                PasswordNew = "teste12"
            };
            PasswordCommandValidator validation = new PasswordCommandValidator();

            //Act
            var resultLogin = validation.Validate(passwordCommand);

            //Assert
            Assert.True(resultLogin.Errors.Count == 1);
            Assert.False(resultLogin.IsValid);
            Assert.Equal(resultLogin.Errors[0].PropertyName, "PasswordNew");
            Assert.Equal(JsonConvert.SerializeObject(UserValidatorConstant.PASSWORD_NEW_HAS_INVALID), resultLogin.Errors[0].ErrorMessage);
        }

        [Fact]
        public void NotSendPasswordNewRequest()
        {
            //Arranje
            PasswordCommand passwordCommand = new PasswordCommand()
            {
                Login = "nancy.sousa",
                PasswordOld = "teste$12"
            };
            PasswordCommandValidator validation = new PasswordCommandValidator();

            //Act
            var resultLogin = validation.Validate(passwordCommand);

            //Assert
            Assert.True(resultLogin.Errors.Count == 1);
            Assert.False(resultLogin.IsValid);
            Assert.Equal(resultLogin.Errors[0].PropertyName, "PasswordNew");
            Assert.Equal(JsonConvert.SerializeObject(UserValidatorConstant.PASSWORD_NEW_MANDATORY), resultLogin.Errors[0].ErrorMessage);
        }
        #endregion
    }
}