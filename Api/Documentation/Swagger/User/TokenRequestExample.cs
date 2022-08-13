using Application.Command.User;
using Swashbuckle.AspNetCore.Filters;

namespace Api.Documentation.Swagger.User
{
    public class TokenRequestExample : IExamplesProvider<object>
    {
        public object GetExamples()
        {
            return new LoginCommand() { Login = "joao.silva", Password = "#odontopuc1" };
        }
    }
}