using Cinema.Infrastructure.Dto;
using CinemaWpf.Commands;
using CinemaWpf.Controllers;
using CinemaWpf.Model;
using CinemaWpf.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace CinemaWpf.ViewModel
{
    /// <summary>
    /// ViewModel for reservation form.
    /// </summary>
    public class ReservationViewModel : TicketReservationViewModel
    {

        /// <summary>
        /// Initializes a new instance of the ReservationViewModel class with existing movie.
        /// </summary>
        /// <param name="model">Reservation to edit.</param>
        public ReservationViewModel(CustomerReservation model) : base(model)
        {
            SaveCommand = new SaveReservationCommand(this);
            Reservation.Reservation.Status = 0;
        }

        /// <summary>
        /// Saves changes to the reservation instance
        /// </summary>
        public override void saveReservation()
        {
            bool isSend = false;
            isSend = Controller.Post(Reservation);
            if (isSend)
                ReservationView.GetInstance().Close();
        }
        
    }
}
