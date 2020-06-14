using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Cinema.Core.Domain;
using Cinema.Core.Repositories;
using Cinema.Infrastructure.Dto;
using Cinema.Infrastructure.Repositories;

namespace Cinema.Infrastructure.Services
{
    /// <summary>
    /// Provides methods for using MovieRepository.
    /// </summary> 
    public class MovieService : IMovieService
    {
        private IMovieRepository _repository;
        private ICategoryRepository _categoryRepository;
        private IShowingRepository _showingRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes an instance of MovieService class. Initalizes data fields: creates instance of MovieRepository and CategoryRepository.
        /// </summary>
        /// <param name="mapper">Mapper</param>
        public MovieService(IMapper mapper)
        {
            _repository = new MovieRepository();
            _categoryRepository = new CategoryRepository();
            _showingRepository = new ShowingRepository();
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a custom MovieDto.
        /// </summary>
        /// <param name="id">id of the movie</param>
        /// <returns>MovieDto object.</returns>
        public MovieDto Get(int id)
        {
            var movie = _repository.Get(id);
            return _mapper.Map<MovieDto>(movie);
        }

        /// <summary>
        /// Gets all Movies.
        /// </summary>
        /// <returns>List of MovieDto objects.</returns>
        public IList<MovieDto> GetAll()
        {
            var movies = _repository.GetAll();
            return _mapper.Map<IList<MovieDto>>(movies);
        }

        /// <summary>
        /// Gets movies by custom category.
        /// </summary>
        /// <param name="name">Category name</param>
        /// <returns>List of MovieDto objects.</returns>
        public IList<MovieDto> GetByCategory(string name)
        {
            var category = _categoryRepository.GetByName(name);
            var movies = _repository.GetMoviesByCategory(category);
            return _mapper.Map<IList<MovieDto>>(movies);
        }

        /// <summary>
        /// Inserts or updates a movie. If id isn't set (the value is equal 0) the movie is insterted. Otherwise the movie is updated.
        /// </summary>
        /// <param name="item">movieDto object</param>
        /// <returns>Id of interted movie or number of affected rows.</returns>
        public int InsertOrUpdate(MovieDto item)
        {
            var categoryId = _categoryRepository.GetOrAddByName(item.CategoryName);
            var movie = _mapper.Map<Movie>(item);
            movie.CategoryId = categoryId;
            return _repository.InsertOrUpdate(movie);
        }

        /// <summary>
        /// Removes the movie.
        /// </summary>
        /// <param name="id">id of the movie to remove.</param>
        public void Remove(int id)
        {
            var showings = _showingRepository.GetShowingsByMovie(id);
            if (showings == null)
                _repository.Remove(id);
        }
    }
}
