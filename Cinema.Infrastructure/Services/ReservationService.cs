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
    /// Provides methods for using reservations. 
    /// </summary>>
    public class ReservationService : IReservationService
    {

        private IReservationRepository _repository;
        private IShowingRepository _showingRepository;
        private IRowSeatRepository _rowSeatRepository;
        private ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public ReservationService(IMapper mapper)
        {
            _repository = new ReservationRepository();
            _showingRepository = new ShowingRepository();
            _rowSeatRepository = new RowSeatRepository();
            _customerRepository = new CustomerRepository();
            _mapper = mapper;
        }

        public IList<ReservationDto> GetByShowing(int id)
        {
            var showing = _showingRepository.Get(id);
            var reservations = _repository.GetReservationByShowing(showing);
            return _mapper.Map<IList<ReservationDto>>(reservations);
        }

        public IList<ReservationDto> GetByCustomer(CustomerDto customerDto)
        {
            var customerInput = _mapper.Map<Customer>(customerDto);
            var customer = _customerRepository.GetOrAddByData(customerInput);
            var reservations = _repository.GetReservationByCustomer(customer);
            return _mapper.Map<IList<ReservationDto>>(reservations);
        }

        public ReservationDto Get(int id)
        {
            var reservation = _repository.Get(id);
            return _mapper.Map<ReservationDto>(reservation);
        }

        public IList<ReservationDto> GetAll()
        {
            var reservations = _repository.GetAll();
            return _mapper.Map<IList<ReservationDto>>(reservations);
        }

        public int InsertOrUpdate(ReservationDto item)
        {
            var showing = _showingRepository.Get(item.ShowingId);
            var seat = _rowSeatRepository.Get(item.RowSeatId);
            var customer = _customerRepository.Get(item.CustomerId);
            var reservation = _mapper.Map<Reservation>(item);
            reservation.RowSeatId = seat;
            reservation.ShowingId = showing;
            reservation.CustomerId = customer;
            return _repository.InsertOrUpdate(reservation);
        }

        public void Remove(int id)
        {
            _repository.Remove(id);
        }

        public List<RowSeatDto> GetSeats(int showingId)
        {
            IList<ReservationDto> reservations = GetByShowing(showingId);
            List<RowSeatDto> seats = _mapper.Map<List<RowSeatDto>>(_rowSeatRepository.GetAll());
            ReservationDto reservation = null;
            foreach (RowSeatDto seat in seats)
            {
                reservation = reservations.Where(r => r.RowSeatId == seat.Id).SingleOrDefault();
                if (reservation != null)
                {
                    if (reservation.Status == 0) seat.SeatStatus = RowSeatDto.Status.Reserved;
                    else if (reservation.Status == 1) seat.SeatStatus = RowSeatDto.Status.Sold;
                }
                else seat.SeatStatus = RowSeatDto.Status.Free;
            }
            return seats;
        }

  
    }
}
