using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Dto
{
    public class TicketDto
    {
        public int Id { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
        public string MovieTitle { get; set; }
        public int TheatreId { get; set; }
        public DateTime ShowingDateTime { get; set; }

    }
}
