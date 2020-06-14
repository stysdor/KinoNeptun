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
    /// Repository implementation for showing object
    /// </summary>
    public class ShowingRepository : IShowingRepository
    {
        /// <summary>
        /// Gets showing by id.
        /// </summary>
        /// <param name="id">id of the showing.</param>
        /// <returns>Showing object.</returns>
        public Showing Get(int id)
        {
            Showing showing = null;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString))
            {
                db.Open();
                showing = db.Query<Showing, Movie, Category, Theatre, Showing>(
                    @"SELECT S.Id, S.ShowingDateTime, S.MovieId, M.Id, M.MovieTitle, M.MovieDescription, M.Country, M.YearOfProduction, M.CategoryId, C.Id, C.CategoryName, S.TheatreId, T.Id, T.TheatreName " +
                    @"FROM Showing AS S INNER JOIN Movie AS M ON S.MovieId = M.Id " +
                    @"INNER JOIN Category AS C ON M.CategoryId = C.Id " +
                    @"INNER JOIN Theatre AS T ON S.TheatreId = T.Id " +
                    $"WHERE S.Id = {id}",
                    (showingObj, movie, category, theatre) => {
                        showingObj.MovieId = movie;
                        showingObj.MovieId.CategoryId = category;
                        showingObj.TheatreId = theatre;
                        return showingObj;
                    }, splitOn: "MovieId, CategoryId, TheatreId")
                    .Distinct()
                    .SingleOrDefault();
            }
            return showing;
        }

        /// <summary>
        /// Gets list of actuall showings.
        /// </summary>
        /// <param name="n">number of the nearest showing to get.</param>
        /// <returns>List of actuall showings</returns>
        public IList<Showing> GetActuall(int n)
        {
            string actuallDate = (DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss");
            List<Showing> showings = null;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString))
            {
                db.Open();
                showings = db.Query<Showing, Movie, Category, Theatre, Showing>(
                    $"SELECT TOP {n} S.Id, S.ShowingDateTime, S.MovieId, M.Id, M.MovieTitle, M.MovieDescription, M.Country, M.YearOfProduction, M.CategoryId, C.Id, C.CategoryName, S.TheatreId, T.Id, T.TheatreName " +
                    @"FROM Showing AS S INNER JOIN Movie AS M ON S.MovieId = M.Id " +
                    @"INNER JOIN Category AS C ON M.CategoryId = C.Id " +
                    @"INNER JOIN Theatre AS T ON S.TheatreId = T.Id " +
                    $"WHERE S.ShowingDateTime > '{actuallDate}' " +
                    @"ORDER BY S.ShowingDateTime;" 
                    , (showing, movie, category, theatre) => {
                        showing.MovieId = movie;
                        showing.MovieId.CategoryId = category;
                        showing.TheatreId = theatre;
                        return showing;
                    }, splitOn: "MovieId, CategoryId, TheatreId")
                    .Distinct()
                    .ToList();
            }
            return showings;
        }

        /// <summary>
        /// Gets all showings.
        /// </summary>
        /// <returns>List of showings.</returns>
        public IList<Showing> GetAll()
        {
            List<Showing> showings = null;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString))
            {
                db.Open();
                showings = db.Query<Showing, Movie, Category, Theatre, Showing>(
                    @"SELECT S.Id, S.ShowingDateTime, S.MovieId, M.Id, M.MovieTitle, M.MovieDescription, M.Country, M.YearOfProduction, M.CategoryId, C.Id, C.CategoryName, S.TheatreId, T.Id, T.TheatreName " +
                    @"FROM Showing AS S INNER JOIN Movie AS M ON S.MovieId = M.Id " +
                    @"INNER JOIN Category AS C ON M.CategoryId = C.Id " +
                    @"INNER JOIN Theatre AS T ON S.TheatreId = T.Id "
                    , (showing, movie, category, theatre) => {
                        showing.MovieId = movie;
                        showing.MovieId.CategoryId = category;
                        showing.TheatreId = theatre;
                        return showing;
                    }, splitOn: "MovieId, CategoryId, TheatreId")
                    .Distinct()
                    .ToList();
            }
            return showings;
        }

        /// <summary>
        /// Insterts or Updates a showing depending on setting id. If id isn't set (the value equals 0) the showing is added. Otherwise the showing is modified.
        /// </summary>
        /// <param name="item">model of showind to add or edit.</param>
        /// <returns>id of inserted showing or number of affected rows.</returns>
        public int InsertOrUpdate(Showing item)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString))
            {
                db.Open();
                if (item.Id > 0)
                    return Update(item, db);
                else
                    return Insert(item, db);
            }
        }

        private int Insert(Showing item, IDbConnection db)
        {
            var date = item.ShowingDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            string sql = @"INSERT INTO Showing (MovieId, TheatreId, ShowingDateTime)"
                + $"Values ({item.MovieId.Id}, {item.TheatreId.Id}, '{date}');"
                + @"SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = db.Query<int>(sql).Single();

            return id;
        }

        private int Update(Showing item, IDbConnection db)
        {
            int id = item.Id;
            int movieId = item.MovieId.Id;
            int theatreId = item.TheatreId.Id;
            string sql = $"UPDATE Showing SET MovieId = {movieId}, TheatreId = {theatreId}, " +
                $"ShowingDateTime = '{item.ShowingDateTime}' " +
                $"WHERE Id = {id};";
            var affectedRows = db.Execute(sql);
            return affectedRows;
        }

        /// <summary>
        /// Removes showing by id. 
        /// </summary>
        /// <param name="id">id of the showing to remove.</param>
        public void Remove(int id)
        {
            string sql = "DELETE FROM Showing WHERE Id=@Id";
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString))
            {
                db.Open();
                var affectedRows = db.Execute(sql, new { Id = id });
            }
        }


        /// <summary>
        /// Gets list of showing by custom date. It's made for finding showing playing in the custom day.
        /// </summary>
        /// <param name="date">date of showings</param>
        /// <returns>list of showings</returns>
        public IList<Showing> GetShowingsByDate(DateTime date)
        {
            IList<Showing> showings = null;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString))
            {
                db.Open();
                showings = db.Query<Showing, Movie, Category, Theatre, Showing>(
                @"SELECT S.Id, S.ShowingDateTime, S.MovieId, M.Id, M.MovieTitle, M.MovieDescription, M.Country, M.YearOfProduction, M.CategoryId, C.Id, C.CategoryName, S.TheatreId, T.Id, T.TheatreName " +
                @"FROM Showing AS S INNER JOIN Movie AS M ON S.MovieId = M.Id " +
                @"INNER JOIN Category AS C ON M.CategoryId = C.Id " +
                @"INNER JOIN Theatre AS T ON S.TheatreId = T.Id "
                , (showing, movie, category, theatre) => {
                    showing.MovieId = movie;
                    showing.MovieId.CategoryId = category;
                    showing.TheatreId = theatre;
                    return showing;
                }, splitOn: "MovieId, CategoryId, TheatreId")
                .Distinct()
                .ToList();
            }
            return showings;
        }

        /// <summary>
        /// Gets list of showings of the custom movie
        /// </summary>
        /// <param name="movieId">custom movie id</param>
        /// <returns>list of showings of the custom movie</returns>
        public IList<Showing> GetShowingsByMovie(int movieId)
        {
            IList<Showing> showings = null;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString))
            {
                db.Open();
                showings = db.Query<Showing, Movie, Theatre, Showing>(
                @"SELECT * " +
                @"FROM Showing AS S INNER JOIN Movie AS M ON S.MovieId = M.Id " +
                @"INNER JOIN Theatre AS T ON S.TheatreId = T.Id " +
                $"WHERE S.MovieId = {movieId};",
                (showing, movie, theatre) => {
                    showing.MovieId = movie;
                    showing.TheatreId = theatre;
                    return showing;
                })
                .Distinct()
                .ToList();
            }
            return showings;
        }
    }
}
