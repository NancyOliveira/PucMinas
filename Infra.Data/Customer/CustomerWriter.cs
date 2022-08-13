using Dapper;
using Domain.DTO.Customer;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace Infra.Data.Customer
{
    public class CustomerWriter : ICustomerWriter
    {
        private readonly MySqlConnectionStringBuilder _builder;

        public CustomerWriter(IConfiguration configuration)
        {
            //_builder = new MySqlConnectionStringBuilder(configuration.GetConnectionString("MySql"));
        }

        public async Task AddAsync(CustomerDTO customerDTO)
        {
            
        }

        //public async Task AddAsync(CustomerDTO customerDTO)
        //{
        //    using (IDbConnection db = new MySqlConnection(_builder.ConnectionString))
        //    {
        //        await db.ExecuteAsync(@"INSERT INTO Customer ( Name, Document, Birthdate, Adress, NumberAdress, CEP, Phone, Date )
        //                                VALUES ( @Name, @Document, @Birthdate, @Adress, @NumberAdress, @CEP, @Phone, NOW() );",
        //            new
        //            {
        //                Name = customerDTO.Name,
        //                Document = customerDTO.Document,
        //                Birthdate = customerDTO.Birthdate,
        //                Adress = customerDTO.Adress,
        //                NumberAdress = customerDTO.NumberAdress,
        //                CEP = customerDTO.CEP,
        //                Phone = customerDTO.Phone
        //            }
        //        );
        //    }
        //}
    }
}