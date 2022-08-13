using Application.Command.Customer;
using Swashbuckle.AspNetCore.Filters;

namespace Api.Documentation.Swagger.Customer
{
    public class CostumerRequestExample : IExamplesProvider<object>
    {
        public object GetExamples()
        {
            return new CustomerCommand() { 
                Name = "Paulo Fernandes",
                Document = "411.006.148-26",
                Adress = "Rua dos Alfeneiros",
                NumberAdress = "4",
                CEP = 05241294,
                Birthdate = DateTime.Now.AddYears(-27),
                DDD = 11,
                Phone = 973184319
            };
        }
    }
}