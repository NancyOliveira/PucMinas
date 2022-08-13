using Application.Command.Service;
using Application.Validator.Service;
using Domain.Constant.Service;
using Newtonsoft.Json;
using Xunit;

namespace Test.Validator.Service
{
    public class GetAvailableTimesCommandValidatorTest
    {
        [Fact]
        public void SuccessRequest()
        {
            //Arranje
            GetAvailableTimesCommand getAvailableTimesCommand = new GetAvailableTimesCommand()
            {
                ServiceID = 1
            };
            GetAvailableTimesCommandValidator validation = new GetAvailableTimesCommandValidator();

            //Act
            var resultLogin = validation.Validate(getAvailableTimesCommand);

            //Assert
            Assert.True(resultLogin.Errors.Count == 0);
            Assert.True(resultLogin.IsValid);
        }

        [Fact]
        public void NegativeNumbersRequest()
        {
            //Arranje
            GetAvailableTimesCommand getAvailableTimesCommand = new GetAvailableTimesCommand()
            {
                ServiceID = -1
            };
            GetAvailableTimesCommandValidator validation = new GetAvailableTimesCommandValidator();

            //Act
            var resultLogin = validation.Validate(getAvailableTimesCommand);

            //Assert
            Assert.True(resultLogin.Errors.Count == 1);
            Assert.False(resultLogin.IsValid);
            Assert.Equal(resultLogin.Errors[0].PropertyName, "ServiceID");
            Assert.Equal(JsonConvert.SerializeObject(ServiceValidatorConstant.SERVICE_HAS_INVALID_FORMAT), resultLogin.Errors[0].ErrorMessage);
        }

        [Fact]
        public void ZeroRequest()
        {
            //Arranje
            GetAvailableTimesCommand getAvailableTimesCommand = new GetAvailableTimesCommand()
            {
                ServiceID = 0
            };
            GetAvailableTimesCommandValidator validation = new GetAvailableTimesCommandValidator();

            //Act
            var resultLogin = validation.Validate(getAvailableTimesCommand);

            //Assert
            Assert.True(resultLogin.Errors.Count == 1);
            Assert.False(resultLogin.IsValid);
            Assert.Equal(resultLogin.Errors[0].PropertyName, "ServiceID");
            Assert.Equal(JsonConvert.SerializeObject(ServiceValidatorConstant.SERVICE_HAS_INVALID_FORMAT), resultLogin.Errors[0].ErrorMessage);
        }
    }
}
