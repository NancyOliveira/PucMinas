using Domain.DTO;

namespace Domain.Constant.Consult
{
    public static class ConsultValidatorConstant
    {
        public readonly static ErrorDetailDto CPF_HAS_INVALID_FORMAT = new ErrorDetailDto { Code = 9, Message = "The Cpf field has invalid format." };
        public readonly static ErrorDetailDto CPF_MANDATORY = new ErrorDetailDto { Code = 10, Message = "The cpf is a mandatory field." };
        public readonly static ErrorDetailDto DATECONSULT_MANDATORY = new ErrorDetailDto { Code = 11, Message = "The dateConsult is a mandatory field." };
        public readonly static ErrorDetailDto DATECONSULT_HAS_INVALID = new ErrorDetailDto { Code = 12, Message = "The dateConsult field has invalid date." };
        public readonly static ErrorDetailDto SERVICEID_MANDATORY = new ErrorDetailDto { Code = 13, Message = "The serviceID is a mandatory field." };
        public readonly static ErrorDetailDto SERVICEID_HAS_INVALID = new ErrorDetailDto { Code = 14, Message = "The serviceID field has invalid date." };
    }
}