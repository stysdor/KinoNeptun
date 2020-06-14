using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Domain
{
    /// <summary>
    /// Represents Reservation table from datebase.
    /// </summary>
    public class Reservation :EntityBase
    {
        public Showing ShowingId { get; set; }
        public RowSeat RowSeatId { get; set; }
        public ReservationStatus Status { get; set; }
        public DateTime ReservationDate { get; set; }
        public Customer CustomerId { get; set; }

        public enum ReservationStatus
        {
            Unconfirmed = 0,
            Confirmed  = 1
        };
    }
}
