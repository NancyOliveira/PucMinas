using Application.Command.User;
using Swashbuckle.AspNetCore.Filters;

namespace Api.Documentation.Swagger.User
{
    public class PasswordRequestExample : IExamplesProvider<object>
    {
        public object GetExamples()
        {
            return new PasswordCommand() { Login = "joao.silva", PasswordOld = "#odontopuc1", PasswordNew = "#pucodonto13@" };
        }
    }
}