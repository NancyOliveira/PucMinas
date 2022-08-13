using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Extensions
{
    public static class ProblemDetailsExtensions
    {
        public static IServiceCollection ConfigureProblemDetailsModelState(this IServiceCollection services)
        {
            return services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                                                    .SelectMany(v => v.Errors)
                                                    .Select(v => v.ErrorMessage)
                                                    .ToList();
                    object responseObj;
                    if (errors[0].Contains("Code"))
                    {
                        responseObj = new
                        {
                            Status = StatusCodes.Status400BadRequest,
                            Instance = context.HttpContext.Request.Path,
                            Title = "One or more validation errors ocurred.",
                            Errors = errors.Select(v => JsonConvert.DeserializeObject(v)).ToList()
                        };
                    }
                    else
                    {
                        responseObj = new ValidationProblemDetails(context.ModelState)
                        {
                            Status = StatusCodes.Status400BadRequest,
                            Instance = context.HttpContext.Request.Path,
                        };
                    }

                    return new BadRequestObjectResult(responseObj)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                };
            });
        }
    }
}