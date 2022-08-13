using Application.Command.User;
using Domain.Constant.User;
using FluentValidation;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Application.Validator.User
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Login).Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(JsonConvert.SerializeObject(UserValidatorConstant.LOGIN_MANDATORY))
                .Custom((login, context) =>
                {
                    bool isValid = IsValidLogin(login);
                    if (!isValid)
                    {
                        context.AddFailure(JsonConvert.SerializeObject(UserValidatorConstant.LOGIN_HAS_INVALID_FORMAT));
                    }
                });

            RuleFor(x => x.Password).Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(JsonConvert.SerializeObject(UserValidatorConstant.PASSWORD_MANDATORY))
                .Custom((password, context) =>
                {
                    bool isValid = IsValidPassword(password);
                    if (!isValid)
                    {
                        context.AddFailure(JsonConvert.SerializeObject(UserValidatorConstant.PASSWORD_HAS_INVALID));
                    }
                });
        }

        private bool IsValidLogin(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                return false;
            }
            else
            {
                login = login.Trim();

                if (login.Contains(".") && login.Split(".").Length == 2)
                {
                    var regexItem = new Regex(".*[0-9].*");
                    if (regexItem.IsMatch(login))
                    {
                        return false;
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }
            else
            {
                password = password.Trim();

                var regexItem = new Regex("[^a-zA-Z0-9_.]");

                if (regexItem.IsMatch(password))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
