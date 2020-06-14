using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaWpf.Model
{
    public class Ticket
    {
        public int Id { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
        public string MovieTitle { get; set; }
        public int TheatreId { get; set; }
        public DateTime ShowingDateTime { get; set; }
    }
}
