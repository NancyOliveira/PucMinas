using Application.Command.Consult;
using Swashbuckle.AspNetCore.Filters;

namespace Api.Documentation.Swagger.Consult
{
    public class ConsultRequestExample : IExamplesProvider<object>
    {
        public object GetExamples()
        {
            return new ConsultCommand() { 
                Document = "372.116.798-83", 
                DateConsult = DateTime.Now.AddDays(2).AddHours(12), 
                ServiceID = 1 };
        }
    }
}