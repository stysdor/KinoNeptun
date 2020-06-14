using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Dto
{
    /// <summary>
    /// DTO for RowSeat. It has enum type Status to mark if seat is reserved or sold.
    /// </summary>
    public class RowSeatDto 
    {
        public int Id { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
        public Status SeatStatus { get; set; }

        /// <summary>
        /// Possible values for Status.
        /// </summary>
        public enum Status  { Free, Reserved, Sold }
    }
}

    