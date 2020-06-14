using Cinema.Infrastructure.Dto;
using Cinema.Infrastructure.Mappers;
using Cinema.Infrastructure.Services;
using CinemaWpf.Logic;
using CinemaWpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaWpf.Controllers
{

    /// <summary>
    /// Class delegates actions to ticket service.
    /// </summary>
    public class TicketController
    {
        private ITicketService _service;

        /// <summary>
        /// Initializes an instance of TicketController.
        /// </summary>
        public TicketController()
        {
            _service = new TicketService(AutoMapperConfig.Initialize());
        }

        /// <summary>
        /// Gets instance of Ticket class based on <paramref name="model"/> and using TicketManager generates ticket file.
        /// </summary>
        /// <param name="model">instance of Reservation class</param>
        public void GetTicket(Reservation model)
        {
            var ticket = GetTicketByReservation(model);
            TicketManager.GenerateTicket(ticket);
        }

        private Ticket GetTicketByReservation(Reservation model)
        {
            var ticket = _service.GetTicket(
                new ReservationDto()
                {
                    Id = model.Id,
                    CustomerId = model.CustomerId,
                    RowSeatId = model.RowSeatId,
                    ShowingId = model.ShowingId,
                    Status = model.Status
                });
            Ticket newTicket = new Ticket()
            {
                Id = ticket.Id,
                MovieTitle = ticket.MovieTitle,
                RowNumber = ticket.RowNumber,
                SeatNumber = ticket.SeatNumber,
                ShowingDateTime = ticket.ShowingDateTime,
                TheatreId = ticket.TheatreId
            };

            return newTicket;
        }
    }
}
