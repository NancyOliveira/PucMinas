using Application.Command.Customer;
using Application.Validator.Customer;
using Domain.Constant.Customer;
using Newtonsoft.Json;
using System;
using Xunit;

namespace Test.Validator.Customer
{
    public class CostumerCommandValidatorTest
    {
        [Fact]
        public void SuccessRequest()
        {
            //Arranje
            CustomerCommand customerCommand = new CustomerCommand()
            {
                Name = "Paulo Fernandes",
                Document = "411.006.148-26",
                Adress = "Rua dos Alfeneiros",
                NumberAdress = "4",
                CEP = 05241294,
                Birthdate = DateTime.Now.AddYears(-27),
                DDD = 11,
                Phone = 973184319
            };
            CustomerCommandValidator validation = new CustomerCommandValidator();

            //Act
            var resultLogin = validation.Validate(customerCommand);

            //Assert
            Assert.True(resultLogin.Errors.Count == 0);
            Assert.True(resultLogin.IsValid);
        }

        #region Mandatory
        [Fact]
        public void DocumentMandatoryRequest()
        {
            //Arranje
            CustomerCommand customerCommand = new CustomerCommand()
            {
                Name = "Paulo Fernandes",
                Adress = "Rua dos Alfeneiros",
                NumberAdress = "4",
                CEP = 05241294,
                Birthdate = DateTime.Now.AddYears(-27),
                DDD = 11,
                Phone = 973184319
            };
            CustomerCommandValidator validation = new CustomerCommandValidator();

            //Act
            var resultLogin = validation.Validate(customerCommand);

            //Assert
            Assert.True(resultLogin.Errors.Count == 1);
            Assert.False(resultLogin.IsValid);
            Assert.Equal(resultLogin.Errors[0].PropertyName, "Document");
            Assert.Equal(JsonConvert.SerializeObject(CustomerValidatorConstant.CPF_MANDATORY), resultLogin.Errors[0].ErrorMessage);
        }

        [Fact]
        public void NameMandatoryRequest()
        {
            //Arranje
            CustomerCommand customerCommand = new CustomerCommand()
            {
                Document = "411.006.148-26",
                Adress = "Rua dos Alfeneiros",
                NumberAdress = "4",
                CEP = 05241294,
                Birthdate = DateTime.Now.AddYears(-27),
                DDD = 11,
                Phone = 973184319
            };
            CustomerCommandValidator validation = new CustomerCommandValidator();

            //Act
            var resultLogin = validation.Validate(customerCommand);

            //Assert
            Assert.True(resultLogin.Errors.Count == 1);
            Assert.False(resultLogin.IsValid);
            Assert.Equal(resultLogin.Errors[0].PropertyName, "Name");
            Assert.Equal(JsonConvert.SerializeObject(CustomerValidatorConstant.NAME_MANDATORY), resultLogin.Errors[0].ErrorMessage);
        }

        [Fact]
        public void AdressMandatoryRequest()
        {
            //Arranje
            CustomerCommand customerCommand = new CustomerCommand()
            {
                Name = "Paulo Fernandes",
                Document = "411.006.148-26",
                NumberAdress = "4",
                CEP = 05241294,
                Birthdate = DateTime.Now.AddYears(-27),
                DDD = 11,
                Phone = 973184319
            };
            CustomerCommandValidator validation = new CustomerCommandValidator();

            //Act
            var resultLogin = validation.Validate(customerCommand);

            //Assert
            Assert.True(resultLogin.Errors.Count == 1);
            Assert.False(resultLogin.IsValid);
            Assert.Equal(resultLogin.Errors[0].PropertyName, "Adress");
            Assert.Equal(JsonConvert.SerializeObject(CustomerValidatorConstant.ADRESS_MANDATORY), resultLogin.Errors[0].ErrorMessage);
        }

        [Fact]
        public void NumberAdressMandatoryRequest()
        {
            //Arranje
            CustomerCommand customerCommand = new CustomerCommand()
            {
                Name = "Paulo Fernandes",
                Document = "411.006.148-26",
                Adress = "Rua dos Alfeneiros",
                CEP = 05241294,
                Birthdate = DateTime.Now.AddYears(-27),
                DDD = 11,
                Phone = 973184319
            };
            CustomerCommandValidator validation = new CustomerCommandValidator();

            //Act
            var resultLogin = validation.Validate(customerCommand);

            //Assert
            Assert.True(resultLogin.Errors.Count == 1);
            Assert.False(resultLogin.IsValid);
            Assert.Equal(resultLogin.Errors[0].PropertyName, "NumberAdress");
            Assert.Equal(JsonConvert.SerializeObject(CustomerValidatorConstant.NUMBERADRESS_MANDATORY), resultLogin.Errors[0].ErrorMessage);
        }

        [Fact]
        public void PhoneMandatoryRequest()
        {
            //Arranje
            CustomerCommand customerCommand = new CustomerCommand()
            {
                Name = "Paulo Fernandes",
                Document = "411.006.148-26",
                Adress = "Rua dos Alfeneiros",
                NumberAdress = "4",
                CEP = 05241294,
                Birthdate = DateTime.Now.AddYears(-27),
                DDD = 11
            };
            CustomerCommandValidator validation = new CustomerCommandValidator();

            //Act
            var resultLogin = validation.Validate(customerCommand);

            //Assert
            Assert.True(resultLogin.Errors.Count == 1);
            Assert.False(resultLogin.IsValid);
            Assert.Equal(resultLogin.Errors[0].PropertyName, "Phone");
            Assert.Equal(JsonConvert.SerializeObject(CustomerValidatorConstant.PHONE_HAS_INVALID), resultLogin.Errors[0].ErrorMessage);
        }
        #endregion

        #region Parameters Invalid
        [Fact]
        public void BirthdateNotSendRequest()
        {
            //Arranje
            CustomerCommand customerCommand = new CustomerCommand()
            {
                Document = "411.006.148-26",
                Name = "Paulo Fernandes",
                Adress = "Rua dos Alfeneiros",
                NumberAdress = "4",
                CEP = 05241294,
                DDD = 11,
                Phone = 973184319
            };
            CustomerCommandValidator validation = new CustomerCommandValidator();

            //Act
            var resultLogin = validation.Validate(customerCommand);

            //Assert
            Assert.True(resultLogin.Errors.Count == 1);
            Assert.False(resultLogin.IsValid);
            Assert.Equal(resultLogin.Errors[0].PropertyName, "Birthdate");
            Assert.Equal(JsonConvert.SerializeObject(CustomerValidatorConstant.BIRTHDATE_HAS_INVALID), resultLogin.Errors[0].ErrorMessage);
        }

        [Fact]
        public void CEPNotSendRequest()
        {
            //Arranje
            CustomerCommand customerCommand = new CustomerCommand()
            {
                Name = "Paulo Fernandes",
                Document = "411.006.148-26",
                Adress = "Rua dos Alfeneiros",
                NumberAdress = "4",
                Birthdate = DateTime.Now.AddYears(-27),
                DDD = 11,
                Phone = 973184319
            };
            CustomerCommandValidator validation = new CustomerCommandValidator();

            //Act
            var resultLogin = validation.Validate(customerCommand);

            //Assert
            Assert.True(resultLogin.Errors.Count == 1);
            Assert.False(resultLogin.IsValid);
            Assert.Equal(resultLogin.Errors[0].PropertyName, "CEP");
            Assert.Equal(JsonConvert.SerializeObject(CustomerValidatorConstant.CEP_HAS_INVALID), resultLogin.Errors[0].ErrorMessage);
        }

        [Fact]
        public void DDDNotSendRequest()
        {
            //Arranje
            CustomerCommand customerCommand = new CustomerCommand()
            {
                Name = "Paulo Fernandes",
                Document = "411.006.148-26",
                Adress = "Rua dos Alfeneiros",
                CEP = 05241294,
                NumberAdress = "4",
                Birthdate = DateTime.Now.AddYears(-27),
                Phone = 973184319
            };
            CustomerCommandValidator validation = new CustomerCommandValidator();

            //Act
            var resultLogin = validation.Validate(customerCommand);

            //Assert
            Assert.True(resultLogin.Errors.Count == 1);
            Assert.False(resultLogin.IsValid);
            Assert.Equal(resultLogin.Errors[0].PropertyName, "DDD");
            Assert.Equal(JsonConvert.SerializeObject(CustomerValidatorConstant.DDD_HAS_INVALID), resultLogin.Errors[0].ErrorMessage);
        }

        [Fact]
        public void NameWithNumbersRequest()
        {
            //Arranje
            CustomerCommand customerCommand = new CustomerCommand()
            {
                Name = "Paulo Fernandes 12",
                Document = "411.006.148-26",
                Adress = "Rua dos Alfeneiros",
                NumberAdress = "4",
                CEP = 05241294,
                Birthdate = DateTime.Now.AddYears(-27),
                DDD = 11,
                Phone = 973184319
            };
            CustomerCommandValidator validation = new CustomerCommandValidator();

            //Act
            var resultLogin = validation.Validate(customerCommand);

            //Assert
            Assert.True(resultLogin.Errors.Count == 1);
            Assert.False(resultLogin.IsValid);
            Assert.Equal(resultLogin.Errors[0].PropertyName, "Name");
            Assert.Equal(JsonConvert.SerializeObject(CustomerValidatorConstant.NAME_HAS_INVALID), resultLogin.Errors[0].ErrorMessage);
        }

        [Fact]
        public void NameWithSpecialCharactersRequest()
        {
            //Arranje
            CustomerCommand customerCommand = new CustomerCommand()
            {
                Name = "Paulo Fernandes !@#$%¨&*()_+",
                Document = "411.006.148-26",
                Adress = "Rua dos Alfeneiros",
                NumberAdress = "4",
                CEP = 05241294,
                Birthdate = DateTime.Now.AddYears(-27),
                DDD = 11,
                Phone = 973184319
            };
            CustomerCommandValidator validation = new CustomerCommandValidator();

            //Act
            var resultLogin = validation.Validate(customerCommand);

            //Assert
            Assert.True(resultLogin.Errors.Count == 1);
            Assert.False(resultLogin.IsValid);
            Assert.Equal(resultLogin.Errors[0].PropertyName, "Name");
            Assert.Equal(JsonConvert.SerializeObject(CustomerValidatorConstant.NAME_HAS_INVALID), resultLogin.Errors[0].ErrorMessage);
        }

        [Fact]
        public void AdressWithNumbersRequest()
        {
            //Arranje
            CustomerCommand customerCommand = new CustomerCommand()
            {
                Name = "Paulo Fernandes",
                Document = "411.006.148-26",
                Adress = "Rua dos Alfeneiros 4",
                NumberAdress = "4",
                CEP = 05241294,
                Birthdate = DateTime.Now.AddYears(-27),
                DDD = 11,
                Phone = 973184319
            };
            CustomerCommandValidator validation = new CustomerCommandValidator();

            //Act
            var resultLogin = validation.Validate(customerCommand);

            //Assert
            Assert.True(resultLogin.Errors.Count == 0);
            Assert.True(resultLogin.IsValid);
        }

        [Fact]
        public void NumberWithLettersRequest()
        {
            //Arranje
            CustomerCommand customerCommand = new CustomerCommand()
            {
                Name = "Paulo Fernandes",
                Document = "411.006.148-26",
                Adress = "Rua dos Alfeneiros",
                NumberAdress = "4B",
                CEP = 05241294,
                Birthdate = DateTime.Now.AddYears(-27),
                DDD = 11,
                Phone = 973184319
            };
            CustomerCommandValidator validation = new CustomerCommandValidator();

            //Act
            var resultLogin = validation.Validate(customerCommand);

            //Assert
            Assert.True(resultLogin.Errors.Count == 0);
            Assert.True(resultLogin.IsValid);
        }

        [Fact]
        public void PhoneInvalidRequest()
        {
            //Arranje
            CustomerCommand customerCommand = new CustomerCommand()
            {
                Name = "Paulo Fernandes",
                Document = "411.006.148-26",
                Adress = "Rua dos Alfeneiros",
                NumberAdress = "4",
                CEP = 05241294,
                Birthdate = DateTime.Now.AddYears(-27),
                DDD = 11,
                Phone = 000000000
            };
            CustomerCommandValidator validation = new CustomerCommandValidator();

            //Act
            var resultLogin = validation.Validate(customerCommand);

            //Assert
            Assert.True(resultLogin.Errors.Count == 1);
            Assert.False(resultLogin.IsValid);
            Assert.Equal(resultLogin.Errors[0].PropertyName, "Phone");
            Assert.Equal(JsonConvert.SerializeObject(CustomerValidatorConstant.PHONE_HAS_INVALID), resultLogin.Errors[0].ErrorMessage);
        }

        [Fact]
        public void DDDInvalidRequest()
        {
            //Arranje
            CustomerCommand customerCommand = new CustomerCommand()
            {
                Name = "Paulo Fernandes",
                Document = "411.006.148-26",
                Adress = "Rua dos Alfeneiros",
                NumberAdress = "4",
                CEP = 05241294,
                Birthdate = DateTime.Now.AddYears(-27),
                DDD = 1111,
                Phone = 973184319
            };
            CustomerCommandValidator validation = new CustomerCommandValidator();

            //Act
            var resultLogin = validation.Validate(customerCommand);

            //Assert
            Assert.True(resultLogin.Errors.Count == 1);
            Assert.False(resultLogin.IsValid);
            Assert.Equal(resultLogin.Errors[0].PropertyName, "DDD");
            Assert.Equal(JsonConvert.SerializeObject(CustomerValidatorConstant.DDD_HAS_INVALID), resultLogin.Errors[0].ErrorMessage);
        }
        #endregion
    }
}