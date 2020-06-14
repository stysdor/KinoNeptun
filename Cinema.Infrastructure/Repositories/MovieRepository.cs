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
    /// Repository implementation for movie object.
    /// </summary>
    public class MovieRepository : IMovieRepository
    {
        /// <summary>
        /// Gets a movie with custom id.
        /// </summary>
        /// <param name="id">id of the movie</param>
        /// <returns>Movie object</returns>
        public Movie Get(int id)
        {
            Movie movie = null;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString))
            {
                db.Open();
                movie = db.Query<Movie, Category, Movie>("SELECT M.Id, M.MovieTitle, M.MovieDescription, M.Country, M.YearOfProduction, M.CategoryId, C.Id, C.CategoryName FROM Movie AS M INNER JOIN Category As C ON M.CategoryId = C.Id  " +
                                                $"WHERE M.Id = {id}", 
                                                (movieObj, categoryObj) => {
                                                    movieObj.CategoryId = categoryObj;
                                                    return movieObj;
                                                }, splitOn: "CategoryId")
                                                .Distinct()
                                                .SingleOrDefault();
            }
            return movie;
        }

        /// <summary>
        /// Gets all movies from database.
        /// </summary>
        /// <returns>List of Movie objects.</returns>
        public IList<Movie> GetAll()
        {
            IList<Movie> movies = null;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString))
            {
                db.Open();
                movies = db.Query<Movie, Category, Movie>("SELECT M.Id, M.MovieTitle, M.MovieDescription, M.Country, M.YearOfProduction, M.CategoryId, C.Id, C.CategoryName FROM Movie AS M INNER JOIN Category AS C ON M.CategoryId = C.Id", 
                    (movie, category) =>
                    {
                        movie.CategoryId = category;
                        return movie;
                    }, splitOn: "CategoryId")
                    .Distinct()
                    .ToList();
            }
            return movies;
        }

        /// <summary>
        /// Gets movie by custom category.
        /// </summary>
        /// <param name="category">Category name.</param>
        /// <returns>List of Movie objects.</returns>
        public IList<Movie> GetMoviesByCategory(Category category)
        {
            int categoryId = category.Id;
            IList<Movie> movies = null;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString))
            {
                db.Open();
                movies = db.Query<Movie, Category, Movie>("SELECT Movie.Id, Movie.MovieTitle, Movie.MovieDescription, Movie.Country, Movie.YearOfProduction, Movie.CategoryId, Category.CategoryName, Category.Id FROM Movie INNER JOIN Category ON Movie.CategoryId = Category.Id"
                    + $" WHERE Movie.CategoryId = {categoryId}",
                    (movie, categoryObj) =>
                    {
                        movie.CategoryId = categoryObj;
                        return movie;
                    }, splitOn: "CategoryId")
                    .Distinct()
                    .ToList();
            }
            return movies;
        }

        /// <summary>
        /// Insterts or Updates a movie depending on setting id. If id isn't set (the value equals 0) the movie is added. Otherwise the movie is modified.
        /// </summary>
        /// <param name="item">model of movie to add or edit.</param>
        /// <returns>id of inserted movie or number of affected rows.</returns>
        public int InsertOrUpdate(Movie item)
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

        private int Insert(Movie item, IDbConnection db)
        {
            int categoryId = item.CategoryId.Id;
            string sql = @"INSERT INTO Movie (MovieTitle, MovieDescription, CategoryId, Country, YearOfProduction)"
                +$"Values (@MovieTitle, @MovieDescription, {categoryId}, @Country, @YearOfProduction);"
                +@"SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = db.Query<int>(sql, new
            {
                item.MovieTitle,
                item.MovieDescription,
                item.CategoryId,
                item.Country,
                item.YearOfProduction
            }).Single();

            return id;
        }

        private int Update(Movie item, IDbConnection db)
        {
            int movieId = item.Id;
            int categoryId = item.CategoryId.Id;
            string sql = @"UPDATE Movie SET MovieTitle = @MovieTitle, MovieDescription = @MovieDescription, "+
                $"CategoryId = {categoryId}, Country = @Country, " +
                "YearOfProduction = @YearOfProduction " + 
                $"WHERE Id = {movieId};";
            var affectedRows = db.Execute(sql, new
            {
                item.Id,
                item.MovieTitle,
                item.MovieDescription,
                item.CategoryId,
                item.Country,
                item.YearOfProduction,
            });
            return affectedRows;
        }

        /// <summary>
        /// Removes the custom movie
        /// </summary>
        /// <param name="id">id of the movie to remove.</param>
        public void Remove(int id)
        {
            string sql = "DELETE FROM Movie WHERE Id=@Id";
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnString"].ConnectionString))
            {
                db.Open();
                var affectedRows = db.Execute(sql, new { Id = id });
            }
        }
    }
}
