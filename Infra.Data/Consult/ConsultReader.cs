using Dapper;
using Domain.DTO.Consult;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Consult
{
    public class ConsultReader : IConsultReader
    {
        private readonly MySqlConnectionStringBuilder _builder;

        public ConsultReader(IConfiguration configuration)
        {
            //_builder = new MySqlConnectionStringBuilder(configuration.GetConnectionString("MySql"));
        }
        public async Task<List<ConsultDTO>> GetAsync(string document)
        {
            if(document == "123")
            {
                List<ConsultDTO> consultDTOs = new List<ConsultDTO>();

                consultDTOs.Add(new ConsultDTO()
                {
                    Document = document,
                    ServiceID = 1,
                    DateConsult = DateTime.Now.AddDays(1),
                });

                return consultDTOs;
            }
            else
            {
                return new List<ConsultDTO>();
            }
        }

        //public async Task<ConsultDTO> Getsync(string document)
        //{
        //    using (IDbConnection db = new MySqlConnection(_builder.ConnectionString))
        //    {
        //        return await db.QueryFirstOrDefaultAsync<ConsultDTO>(@"SELECT * FROM Consult WHERE Document = @Document
        //                                                               ORDER BY DateConsult DESC",
        //            new
        //            {
        //                Document = document
        //            }
        //        );
        //    }
        //}

        public async Task<bool> ExistsAync(string document, DateTime dateConsult)
        {
            if (document == "97051605038")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //public async Task<bool> ExistsAync(string document, DateTime dateConsult)
        //{
        //    using (IDbConnection db = new MySqlConnection(_builder.ConnectionString))
        //    {
        //        return await db.QueryFirstOrDefaultAsync<bool>(@"SELECT EXISTS(SELECT * FROM Consult 
        //                                                            WHERE Document = @Document AND DateConsult = @dateConsult)",
        //                    new
        //                    {
        //                        Document = document,
        //                        DateConsult = dateConsult
        //                    }
        //                );
        //    }
        //}

        public async Task<List<DateTime>> GetAvailableTimesAync(int serviceID)
        {
            var dates = new List<DateTime>();

            for (var dt = DateTime.Now; dt <= DateTime.Now.AddDays(10); dt = dt.AddDays(1))
            {
                dates.Add(dt);
            }

            return dates;
        }


        //public async Task<List<DateTime>> GetAvailableTimesAync(int serviceID)
        //{
        //    using (IDbConnection db = new MySqlConnection(_builder.ConnectionString))
        //    {
        //        IEnumerable<DateTime> serviceDTOs = (IEnumerable<DateTime>)await db.QueryAsync<IEnumerable<DateTime>>(@"SELECT DateConsult FROM Consult WHERE Active = 1");

        //        return serviceDTOs.ToList();
        //    }
        //}
    }
}
