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
    public abstract class TicketReservationViewModel : INotifyPropertyChanged
    {
        public ReservationController Controller { get; set; }
        public ObservableCollection<RowSeat> Seats { get; set; }
        private string seatError;
        /// <summary>
        /// Returns error message for selected seat.
        /// </summary>
        public string SeatError
        {
            get { return seatError; }
            set { seatError = value; NotifyPropertyChanged("SeatEroor"); }
        }
        private RowSeat selectedSeat;
        /// <summary>
        /// Seat selected by user.
        /// </summary>
        public RowSeat SelectedSeat
        {
            get { return selectedSeat; }
            set
            {
                if (value.SeatStatus == RowSeat.Status.Free)
                {
                    selectedSeat = value;
                    Reservation.Reservation.RowSeatId = SelectedSeat.Id;
                    SeatError = null;
                }
                else
                {
                    selectedSeat = null;
                    Reservation.Reservation.RowSeatId = 0;
                    SeatError = "Miejsce jest już zajęte.";
                }
                NotifyPropertyChanged("Reservation");
                NotifyPropertyChanged("SelectedSeat");
                NotifyPropertyChanged("SeatError");
            }
        }
        private CustomerReservation reservation;

        /// <summary>
        /// Gets the Reservation instance
        /// </summary>
        public CustomerReservation Reservation
        {
            get { return reservation; }
            set { reservation = value; NotifyPropertyChanged("Reservation"); }
        }

        /// <summary>
        /// Initializes a new instance of the ReservationViewModel class with existing movie.
        /// </summary>
        /// <param name="model">Reservation to edit.</param>
        public TicketReservationViewModel(CustomerReservation model)
        {
            Reservation = model;
            Controller = new ReservationController();
            Seats = Controller.GetSeats(model.Reservation.ShowingId);
            SeatError = "Wybierz miejsce.";
        }

        /// <summary>
        /// Command for saving reservation.
        /// </summary>
        public ICommand SaveCommand
        {
            get;
            set;
        }

        /// <summary>
        /// Saves changes to the reservation instance
        /// </summary>
        public abstract void saveReservation();
  


        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}

