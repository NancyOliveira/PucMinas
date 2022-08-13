using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Infra.Data.User
{
    public class UserWriter : IUserWriter
    {
        private readonly MySqlConnectionStringBuilder _builder;

        public UserWriter(IConfiguration configuration)
        {
            //_builder = new MySqlConnectionStringBuilder(configuration.GetConnectionString("MySql"));
        }

        public async Task UpdatePassword(string login, string passwordOld, string passwordNew)
        {
            //using (IDbConnection db = new MySqlConnection(_builder.ConnectionString))
            //{
            //    await db.ExecuteAsync(@"UPDATE User 
            //                             SET Password = @PasswordNew
            //                            WHERE Login = @Login AND Password = @PasswordOld AND
            //                                  Active = 1",
            //        new
            //        {
            //            Login = login,
            //            PasswordOld = passwordOld,
            //            PasswordNew = passwordNew
            //        }
            //    );
            //}
        }

        //public async Task UpdatePassword(string login, string passwordOld, string passwordNew)
        //{
        //    using (IDbConnection db = new MySqlConnection(_builder.ConnectionString))
        //    {
        //        await db.ExecuteAsync(@"UPDATE User 
        //                                 SET Password = @PasswordNew
        //                                WHERE Login = @Login AND Password = @PasswordOld AND
        //                                      Active = 1",
        //            new
        //            {
        //                Login = login,
        //                PasswordOld = passwordOld,
        //                PasswordNew = passwordNew
        //            }
        //        );
        //    }
        //}
    }
}