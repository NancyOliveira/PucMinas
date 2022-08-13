using Domain.DTO.User;
using Swashbuckle.AspNetCore.Filters;

namespace Api.Documentation.Swagger.User
{
    public class TokenResponseExample : IExamplesProvider<object>
    {
        public object GetExamples()
        {
            return new TokenDTO() { Token = "xxxxxxxxx", ExpirationDate = DateTime.Now.AddDays(1) };
        }
    }
}