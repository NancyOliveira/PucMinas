using Domain.DTO;

namespace Domain.Constant.Customer
{
    public static class CustomerValidatorConstant
    {
        public readonly static ErrorDetailDto CPF_HAS_INVALID_FORMAT = new ErrorDetailDto { Code = 13, Message = "The Cpf field has invalid format." };
        public readonly static ErrorDetailDto CPF_MANDATORY = new ErrorDetailDto { Code = 14, Message = "The cpf is a mandatory field." };
        public readonly static ErrorDetailDto NAME_MANDATORY = new ErrorDetailDto { Code = 15, Message = "The name is a mandatory field." };
        public readonly static ErrorDetailDto NAME_HAS_INVALID = new ErrorDetailDto { Code = 16, Message = "The name field has invalid." };
        public readonly static ErrorDetailDto BIRTHDATE_MANDATORY = new ErrorDetailDto { Code = 17, Message = "The birthdate is a mandatory field." };
        public readonly static ErrorDetailDto BIRTHDATE_HAS_INVALID = new ErrorDetailDto { Code = 18, Message = "The birthdate field has invalid." };
        public readonly static ErrorDetailDto ADRESS_MANDATORY = new ErrorDetailDto { Code = 19, Message = "The adress is a mandatory field." };
        public readonly static ErrorDetailDto ADRESS_HAS_INVALID = new ErrorDetailDto { Code = 20, Message = "The adress field has invalid." };
        public readonly static ErrorDetailDto NUMBERADRESS_MANDATORY = new ErrorDetailDto { Code = 21, Message = "The numberAdress is a mandatory field." };
        public readonly static ErrorDetailDto CEP_MANDATORY = new ErrorDetailDto { Code = 22, Message = "The cep is a mandatory field." };
        public readonly static ErrorDetailDto CEP_HAS_INVALID = new ErrorDetailDto { Code = 23, Message = "The cep field has invalid." };
        public readonly static ErrorDetailDto DDD_MANDATORY = new ErrorDetailDto { Code = 24, Message = "The ddd is a mandatory field." };
        public readonly static ErrorDetailDto DDD_HAS_INVALID = new ErrorDetailDto { Code = 25, Message = "The ddd field has invalid." };
        public readonly static ErrorDetailDto PHONE_MANDATORY = new ErrorDetailDto { Code = 26, Message = "The phone is a mandatory field." };
        public readonly static ErrorDetailDto PHONE_HAS_INVALID = new ErrorDetailDto { Code = 27, Message = "The phone field has invalid." };
    }
}