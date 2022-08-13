using Domain.DTO;

namespace Domain.Constant.Service
{
    public static class ServiceValidatorConstant
    {
        public readonly static ErrorDetailDto SERVICE_HAS_INVALID_FORMAT = new ErrorDetailDto { Code = 25, Message = "The service field has invalid." };
        public readonly static ErrorDetailDto SERVICE_MANDATORY = new ErrorDetailDto { Code = 26, Message = "The service is a mandatory field." };
    }
}