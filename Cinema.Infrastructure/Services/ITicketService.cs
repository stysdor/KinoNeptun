using Cinema.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Services
{
    public interface ITicketService
    {
        TicketDto GetTicket(ReservationDto reservation);
    }
}
