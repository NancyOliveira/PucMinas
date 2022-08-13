using Application.Command.Service;
using Domain.Constant.Service;
using FluentValidation;
using Newtonsoft.Json;

namespace Application.Validator.Service
{
    public class GetAvailableTimesCommandValidator : AbstractValidator<GetAvailableTimesCommand>
    {
        public GetAvailableTimesCommandValidator()
        {
            RuleFor(x => x.ServiceID).Cascade(CascadeMode.Stop)
               .NotNull().WithMessage(JsonConvert.SerializeObject(ServiceValidatorConstant.SERVICE_MANDATORY))
               .Custom((serviceID, context) =>
               {
                   if (serviceID <= 0)
                   {
                       context.AddFailure(JsonConvert.SerializeObject(ServiceValidatorConstant.SERVICE_HAS_INVALID_FORMAT));
                   }
               });
        }
    }
}