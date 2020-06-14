using AutoMapper;
using Cinema.Core.Repositories;
using Cinema.Infrastructure.Dto;
using Cinema.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Services
{
    public class TicketService : ITicketService
    {
        private IReservationRepository _repository;
        private IShowingRepository _showingRepository;
        private IRowSeatRepository _rowSeatRepository;
        private readonly IMapper _mapper;

        public TicketService(IMapper mapper)
        {
            _repository = new ReservationRepository();
            _showingRepository = new ShowingRepository();
            _rowSeatRepository = new RowSeatRepository();
            _mapper = mapper;
        }

        public TicketDto GetTicket(ReservationDto reservation)
        {
            ShowingDto showing = _mapper.Map<ShowingDto>(_showingRepository.Get(reservation.ShowingId));
            RowSeatDto seat = _mapper.Map<RowSeatDto>(_rowSeatRepository.Get(reservation.RowSeatId));

            TicketDto ticket = new TicketDto()
            {
                Id = reservation.Id,
                MovieTitle = showing.MovieTitle,
                RowNumber = seat.RowNumber,
                SeatNumber = seat.SeatNumber,
                ShowingDateTime = showing.ShowingDateTime,
                TheatreId = showing.TheatreId
            };

            return ticket;
        }
    }
}
