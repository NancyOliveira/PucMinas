using Application.Command.Consult;
using Application.Validator.Consult;
using Domain.Constant.Consult;
using Newtonsoft.Json;
using System;
using Xunit;

namespace Test.Validator.Consult
{
    public class ConsultCommandValidatorTest
    {
        [Fact]
        public void SuccessRequest()
        {
            //Arranje
            ConsultCommand consultCommand = new ConsultCommand()
            {
                Document = "411.006.148-26",
                DateConsult = DateTime.Now.AddHours(3),
                ServiceID = 1
            };
            ConsultCommandValidator validation = new ConsultCommandValidator();

            //Act
            var result = validation.Validate(consultCommand);

            //Assert
            Assert.True(result.Errors.Count == 0);
            Assert.True(result.IsValid);
        }

        #region Mandatory
        [Fact]
        public void DocumentMandatoryRequest()
        {
            //Arranje
            ConsultCommand consultCommand = new ConsultCommand()
            {
                DateConsult = DateTime.Now.AddHours(3),
                ServiceID = 1
            };
            ConsultCommandValidator validation = new ConsultCommandValidator();

            //Act
            var result = validation.Validate(consultCommand);

            //Assert
            Assert.True(result.Errors.Count == 1);
            Assert.False(result.IsValid);
            Assert.Equal(result.Errors[0].PropertyName, "Document");
            Assert.Equal(JsonConvert.SerializeObject(ConsultValidatorConstant.CPF_MANDATORY), result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void DateMandatoryRequest()
        {
            //Arranje
            ConsultCommand consultCommand = new ConsultCommand()
            {
                Document = "411.006.148-26",
                ServiceID = 1
            };
            ConsultCommandValidator validation = new ConsultCommandValidator();

            //Act
            var result = validation.Validate(consultCommand);

            //Assert
            Assert.True(result.Errors.Count == 1);
            Assert.False(result.IsValid);
            Assert.Equal(result.Errors[0].PropertyName, "DateConsult");
            Assert.Equal(JsonConvert.SerializeObject(ConsultValidatorConstant.DATECONSULT_HAS_INVALID), result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void ServiceIDRequest()
        {
            //Arranje
            ConsultCommand consultCommand = new ConsultCommand()
            {
                Document = "411.006.148-26",
                DateConsult = DateTime.Now.AddHours(3)
            };
            ConsultCommandValidator validation = new ConsultCommandValidator();

            //Act
            var result = validation.Validate(consultCommand);

            //Assert
            Assert.True(result.Errors.Count == 1);
            Assert.False(result.IsValid);
            Assert.Equal(result.Errors[0].PropertyName, "ServiceID");
            Assert.Equal(JsonConvert.SerializeObject(ConsultValidatorConstant.SERVICEID_HAS_INVALID), result.Errors[0].ErrorMessage);
        }
        #endregion

        #region Parameters Invalid
        [Fact]
        public void ServiceIDInvalidRequest()
        {
            //Arranje
            ConsultCommand consultCommand = new ConsultCommand()
            {
                Document = "411.006.148-26",
                ServiceID = 0,
                DateConsult = DateTime.Now.AddHours(3)
            };
            ConsultCommandValidator validation = new ConsultCommandValidator();

            //Act
            var result = validation.Validate(consultCommand);

            //Assert
            Assert.True(result.Errors.Count == 1);
            Assert.False(result.IsValid);
            Assert.Equal(result.Errors[0].PropertyName, "ServiceID");
            Assert.Equal(JsonConvert.SerializeObject(ConsultValidatorConstant.SERVICEID_HAS_INVALID), result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void DocumentInvalidRequest()
        {
            //Arranje
            ConsultCommand consultCommand = new ConsultCommand()
            {
                Document = "000.000.000-43",
                ServiceID = 2,
                DateConsult = DateTime.Now.AddHours(-3)
            };
            ConsultCommandValidator validation = new ConsultCommandValidator();

            //Act
            var result = validation.Validate(consultCommand);

            //Assert
            Assert.True(result.Errors.Count == 1);
            Assert.False(result.IsValid);
            Assert.Equal(result.Errors[0].PropertyName, "Document");
            Assert.Equal(JsonConvert.SerializeObject(ConsultValidatorConstant.CPF_HAS_INVALID_FORMAT), result.Errors[0].ErrorMessage);
        }
        #endregion
    }
}
