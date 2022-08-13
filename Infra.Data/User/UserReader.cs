using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace Infra.Data.User
{
    public class UserReader : IUserReader
    {
        private readonly MySqlConnectionStringBuilder _builder;

        public UserReader(IConfiguration configuration)
        {
            //_builder = new MySqlConnectionStringBuilder(configuration.GetConnectionString("MySql"));
        }

        public async Task<bool> ExistsAsync(string login, string password)
        {
            List<string> logins = new List<string>();
            logins.Add("nancy.sousa");
            logins.Add("carlos.silva");
            logins.Add("maria.oliveira");

            return logins.Exists(n => n.Contains(login));
        }

        //public async Task<bool> Exists(string login, string password)
        //{
        //    using (IDbConnection db = new MySqlConnection(_builder.ConnectionString))
        //    {
        //        return await db.QueryFirstOrDefaultAsync<bool>(@"SELECT EXISTS(SELECT * FROM User WHERE Login = @Login AND Password = @Password AND Active = 1)",
        //            new
        //            {
        //                Login = login,
        //                Password = password
        //            }
        //        );
        //    }
        //}
    }
}