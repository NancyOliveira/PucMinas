using Domain.DTO;

namespace Domain.Constant.User
{
    public class UserValidatorConstant
    {
        public readonly static ErrorDetailDto LOGIN_HAS_INVALID_FORMAT = new ErrorDetailDto { Code = 1, Message = "The Login field has invalid format." };
        public readonly static ErrorDetailDto LOGIN_MANDATORY = new ErrorDetailDto { Code = 2, Message = "The Login is a mandatory field." };
        public readonly static ErrorDetailDto PASSWORD_MANDATORY = new ErrorDetailDto { Code = 3, Message = "The password is a mandatory field." };
        public readonly static ErrorDetailDto PASSWORD_HAS_INVALID = new ErrorDetailDto { Code = 4, Message = "The password field has invalid format." };
        public readonly static ErrorDetailDto PASSWORD_NEW_MANDATORY = new ErrorDetailDto { Code = 5, Message = "The new password is a mandatory field." };
        public readonly static ErrorDetailDto PASSWORD_OLD_MANDATORY = new ErrorDetailDto { Code = 6, Message = "The old password is a mandatory field." };
        public readonly static ErrorDetailDto PASSWORD_NEW_HAS_INVALID = new ErrorDetailDto { Code = 7, Message = "The new password is a mandatory field." };
        public readonly static ErrorDetailDto PASSWORD_OLD_HAS_INVALID = new ErrorDetailDto { Code = 8, Message = "The old password field has invalid format." };
    }
}