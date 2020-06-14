using Cinema.Infrastructure.Dto;
using Cinema.Infrastructure.Mappers;
using Cinema.Infrastructure.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Test
{
    /// <summary>
    /// Class tests Movie Service, including Repository and connecting with datebase etc. 
    /// </summary>
    [TestClass]
    public class MovieTest
    {
        IMovieService _services;
        int addedMovieId = 0;

        /// <summary>
        /// Sets up.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            _services = new MovieService(AutoMapperConfig.Initialize());
        }

        /// <summary>
        /// Tests getting movie by id.
        /// </summary>
        [TestMethod]
        public void GetMovie()
        {
            var movie = _services.Get(20);
            Assert.AreEqual("Lewiatan", movie.MovieTitle);
        }

        /// <summary>
        /// Tests adding movie to datebase.
        /// </summary>
        [TestMethod]
        public void AddMovie()
        {
            var movieDto = new MovieDto()
            {
                MovieTitle = "Lewiatan",
                MovieDescription = "W obliczu eksmisji Kola walczy ze skorumpowaną strukturą władzy.",
                CategoryName = "Thriller",
                Country = "Rosja",
                YearOfProduction = "2014",
            };

            addedMovieId = _services.InsertOrUpdate(movieDto);
            var movieDb = _services.Get(addedMovieId);
            Assert.ReferenceEquals(movieDb, movieDto);
        }

        /// <summary>
        /// Tests getting list of movies from datebase.
        /// </summary>
        [TestMethod]
        public void GetAllMovie()
        { 
            IList<MovieDto> movies = _services.GetAll();
            Assert.IsNotNull(movies);
        }

        /// <summary>
        /// Tests getting movies by category.
        /// </summary>
        [TestMethod]
        public void GetMoviesByCategory()
        {
            string category = "Thriller";
            IList<MovieDto> movies = _services.GetByCategory(category);
            Assert.IsNotNull(movies);
            Assert.AreEqual(category, movies.ElementAt(0).CategoryName);
        }

        /// <summary>
        /// Tests editing a movie.
        /// </summary>
        [TestMethod]
        public void EditMovie()
        {
            var movieDto = new MovieDto()
            {
                MovieTitle = "Lewiatan",
                MovieDescription = "W obliczu eksmisji Kola walczy ze skorumpowaną strukturą władzy.",
                CategoryName = "Thriller",
                Country = "Rosja",
                YearOfProduction = "2014",
            };

            addedMovieId = _services.InsertOrUpdate(movieDto);
            var movieDb = _services.Get(addedMovieId);
            string categoryName = "Dramat";
            movieDto = _services.Get(addedMovieId);
            movieDto.CategoryName = categoryName;
            var affectedRows = _services.InsertOrUpdate(movieDto);
            var updatedMovieDto = _services.Get(addedMovieId);

            Assert.AreEqual(affectedRows, 1);
            Assert.AreNotEqual(updatedMovieDto.CategoryName, movieDb.CategoryName);
            Assert.AreEqual(updatedMovieDto.MovieTitle, movieDb.MovieTitle);
            Assert.AreEqual(updatedMovieDto.MovieDescription, movieDb.MovieDescription);
        }

        /// <summary>
        /// Removes added movie after tests.
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            if (addedMovieId > 0)
            {
                _services.Remove(addedMovieId);
            }
        }
    }
}

