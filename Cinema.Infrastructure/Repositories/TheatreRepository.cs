using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Core.Domain;
using Cinema.Core.Repositories;
using Dapper;

namespace Cinema.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for theatre object.
    /// </summary>
    public class TheatreRepository : ITheatreRepository
    {

        /// <summary>
        /// Gets Theatre by id.
        /// </summary>
        /// <param name="id">id of theatre</param>
        /// <returns></returns>
        public Theatre Get(int id)
        {
            Theatre theatre = null;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString))
            {
                db.Open();
                theatre = db.Query<Theatre>("SELECT * FROM Theatre " +
                                                $"WHERE Id = {id};", new { id }).SingleOrDefault();
            }
            return theatre;
        }

        /// <summary>
        /// Get all theatres.
        /// It's make for the future use.
        /// </summary>
        /// <returns>List of theatres.</returns>
        public IList<Theatre> GetAll()
        {
            IList<Theatre> theatres = null;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString))
            {
                db.Open();
                theatres = db.Query<Theatre>("SELECT * FROM Theatre ").ToList();
            }
            return theatres;
        }
    }
}
