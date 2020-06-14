using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Dto
{
    /// <summary>
    /// DTO for Reservation
    /// </summary>
    public class ReservationDto
    {

        public int Id { get; set; }
        public int ShowingId { get; set; }
        public int RowSeatId { get; set; }
        public int Status { get; set; }
        public int CustomerId { get; set; }
        
    }
}
