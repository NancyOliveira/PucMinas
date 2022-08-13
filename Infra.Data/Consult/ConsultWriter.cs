using Dapper;
using Domain.DTO.Consult;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace Infra.Data.Consult
{
    public class ConsultWriter : IConsultWriter
    {
        private readonly MySqlConnectionStringBuilder _builder;

        public ConsultWriter(IConfiguration configuration)
        {
            //_builder = new MySqlConnectionStringBuilder(configuration.GetConnectionString("MySql"));
        }

        public async Task AddAsync(ConsultDTO consultDTO)
        {

        }

        //public async Task AddAsync(ConsultDTO consultDTO)
        //{
        //    using (IDbConnection db = new MySqlConnection(_builder.ConnectionString))
        //    {
        //        await db.ExecuteAsync(@"INSERT INTO Consult ( Document, ServiceID, DateConsult, Date )
        //                                VALUES ( @Document, @ServiceID, @DateConsult, NOW() );",
        //            new
        //            {
        //                Document = consultDTO.Document,
        //                ServiceID = consultDTO.ServiceID,
        //                DateConsult = consultDTO.DateConsult
        //            }
        //        );
        //    }
        //}
    }
}