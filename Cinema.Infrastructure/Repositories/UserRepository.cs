using Cinema.Core.Domain;
using Cinema.Core.Repositories;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public bool ValidateUser(string userName, string password)
        {
            bool isValidate = false;

            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString))
            {
                var sql = $"SELECT UserPassword FROM Users WHERE UserLogin = '{userName}'";
                db.Open();
                var pass = db.Query<string>(sql).SingleOrDefault();
                if (!(pass == null) && string.Equals(pass, password))
                {
                    isValidate = true;
                }
            }
            return isValidate;
        }

        public User GetUser(string userName)
        {
            User user = new User();

            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString))
            {
                var sql = $"SELECT * FROM Users WHERE UserLogin = '{userName}'";
                db.Open();
                user = db.Query<User>(sql).SingleOrDefault();
            }
            return user;
        }
    }
}
