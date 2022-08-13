using Dapper;
using Domain.DTO.Service;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace Infra.Data.Service
{
    public class ServiceReader : IServiceReader
    {
        private readonly MySqlConnectionStringBuilder _builder;

        public ServiceReader(IConfiguration configuration)
        {
            //_builder = new MySqlConnectionStringBuilder(configuration.GetConnectionString("MySql"));
        }

        public async Task<List<ServiceDTO>> GetAll()
        {
            List<ServiceDTO> services = new List<ServiceDTO>();
            services.Add(new ServiceDTO() { ID = 1, Name = "", Active = true, Date = DateTime.Now });
            services.Add(new ServiceDTO() { ID = 1, Name = "", Active = true, Date = DateTime.Now });
            services.Add(new ServiceDTO() { ID = 1, Name = "", Active = true, Date = DateTime.Now });
            services.Add(new ServiceDTO() { ID = 1, Name = "", Active = true, Date = DateTime.Now });

            return services;
        }

        //public async Task<List<ServiceDTO>> GetAll()
        //{
        //    using (IDbConnection db = new MySqlConnection(_builder.ConnectionString))
        //    {

        //        IEnumerable<ServiceDTO> serviceDTOs = (IEnumerable<ServiceDTO>)await db.QueryAsync<IEnumerable<ServiceDTO>>(@"SELECT * FROM Service WHERE Active = 1");

        //        return serviceDTOs.ToList();
        //    }
        //}

        public async Task<bool> ExistsAsync(int serviceID)
        {
            if (serviceID == 1)
            {
                return true;
            }
            else if (serviceID == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //public async Task<bool> ExistsAsync(int serviceID)
        //{
        //    using (IDbConnection db = new MySqlConnection(_builder.ConnectionString))
        //    {
        //        return await db.QueryFirstOrDefaultAsync<bool>(@"SELECT EXISTS(SELECT * FROM Service WHERE ID = @ServiceID;",
        //            new
        //            {
        //                ServiceID = serviceID
        //            }
        //        );
        //    }
        //}
    }
}