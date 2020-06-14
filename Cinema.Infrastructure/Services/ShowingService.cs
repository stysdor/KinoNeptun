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
    /// Provides methods for using showings. 
    /// </summary> 
    public class ShowingService : IShowingService
    {

        private IMovieRepository _movieRepository;
        private ITheatreRepository _theatreRepository;
        private IShowingRepository _repository;
        private IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes instance of Showing service.
        /// </summary>
        /// <param name="mapper">mapper</param>
        public ShowingService(IMapper mapper)
        {
            _movieRepository = new MovieRepository();
            _repository = new ShowingRepository();
            _theatreRepository = new TheatreRepository();
            _reservationRepository = new ReservationRepository();
            _mapper = mapper;
        }

        /// <summary>
        /// Gets showing by id.
        /// </summary>
        /// <param name="id">is of the showing.</param>
        /// <returns>Showing DTO object.</returns>
        public ShowingDto Get(int id)
        {
            var showing = _repository.Get(id);
            return _mapper.Map<ShowingDto>(showing);
        }

        /// <summary>
        /// Gets list of actuall showings. 
        /// </summary>
        /// <param name="n">number of the nearest showing</param>
        /// <returns>list of actuall showings.</returns>
        public IList<ShowingDto> GetActuall(int n)
        {
            var showings = _repository.GetActuall(n);
            return _mapper.Map<IList<ShowingDto>>(showings);
        }

        /// <summary>
        /// Gets all showings.
        /// </summary>
        /// <returns>List of all showings.</returns>
        public IList<ShowingDto> GetAll()
        {
            var showings = _repository.GetAll();
            return _mapper.Map<IList<ShowingDto>>(showings);
        }

        /// <summary>
        /// Gets showing by the custom movie.
        /// </summary>
        /// <param name="id">id of the custom movie.</param>
        /// <returns>List of showings.</returns>
        public IList<ShowingDto> GetByMovie(int id)
        {
            var showings = _repository.GetShowingsByMovie(id); 
            return _mapper.Map<IList<ShowingDto>>(showings);
        }

        /// <summary>
        /// Inserts or updates showing. If id isn't set (the value is equal 0) the showing is insterted. Otherwise the showing is updated.
        /// </summary>
        /// <param name="item">showingDto object</param>
        /// <returns>Id of interted showing or number of affected rows.</returns>
        public int InsertOrUpdate(ShowingDto item)
        {
            var movie = _movieRepository.Get(item.MovieId);
            var showing = _mapper.Map<Showing>(item);
            var theatre = _theatreRepository.Get(item.TheatreId);
            showing.MovieId = movie;
            return _repository.InsertOrUpdate(showing);
        }

        /// <summary>
        /// Removes the showing.
        /// </summary>
        /// <param name="id">id of the showing to remove.</param>
        public void Remove(int id)
        {
            var reservations = _reservationRepository.GetReservationByShowing(new Showing() { Id = id});
            if(reservations == null)
                 _repository.Remove(id);
        }
    }
}
