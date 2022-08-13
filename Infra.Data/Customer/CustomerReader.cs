using Domain.DTO.Customer;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Infra.Data.Customer
{
    public class CustomerReader : ICustomerReader
    {
        private readonly MySqlConnectionStringBuilder _builder;

        public CustomerReader(IConfiguration configuration)
        {
            //_builder = new MySqlConnectionStringBuilder(configuration.GetConnectionString("MySql"));
        }

        public async Task<bool> ExistsAsync(string document)
        {
            List<string> documents = new List<string>();
            documents.Add("41100614826");
            documents.Add("15791274874");
            documents.Add("42903176850");

            return documents.Exists(n => n.Contains(document));
        }

        public async Task<CustomerDTO> GetDocumentAsync(string document)
        {
            return new CustomerDTO()
            {
                Name = "Nancy",
                Document = "123",
                Adress = "Rua dos Alfeneiros",
                NumberAdress = "4",
                CEP = 0000000,
                Birthdate = DateTime.Now,
                Phone = 111111111
            };
        }

        //public async Task<bool> Exists(string document)
        //{
        //    using (IDbConnection db = new MySqlConnection(_builder.ConnectionString))
        //    {
        //        return await db.QueryFirstOrDefaultAsync<bool>(@"SELECT EXISTS(SELECT * FROM Customer WHERE Document = @Document)",
        //            new
        //            {
        //                Document = document
        //            }
        //        );
        //    }
        //}

        //public Task<CustomerDTO> GetDocumentAsync(string document)
        //{
        //    using (IDbConnection db = new MySqlConnection(_builder.ConnectionString))
        //    {
        //        return await db.QueryFirstOrDefaultAsync<CustomerDTO>(@"SELECT EXISTS(SELECT * FROM Customer WHERE Document = @Document)",
        //            new
        //            {
        //                Document = document
        //            }
        //        );
        //    }
        //}
    }
}