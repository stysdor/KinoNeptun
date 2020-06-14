using Cinema.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Services
{
    /// <summary>
    /// Interface for ReservationService.
    /// </summary>
    public interface IReservationService
    {
        ReservationDto Get(int id);
        IList<ReservationDto> GetAll();
        IList<ReservationDto> GetByShowing(int id);
        List<RowSeatDto> GetSeats(int showingId);
        IList<ReservationDto> GetByCustomer(CustomerDto customerDto);
        int InsertOrUpdate(ReservationDto item);
        void Remove(int id);
    }
}
