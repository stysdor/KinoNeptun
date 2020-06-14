using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CinemaWpf.Model
{
    /// <summary>
    /// Model for view connected with both: Reservation and Customer model.
    /// </summary>
    public class CustomerReservation : INotifyPropertyChanged
    {
        private Reservation reservation;
        private Customer customer;

        public Reservation Reservation
        {
            get
            { return reservation; }
            set
            { reservation = value;  NotifyPropertyChanged("Reservation"); }
        }
        public Customer Customer
        {
            get
            { return customer; }
            set
            { customer = value; NotifyPropertyChanged("Customer"); }
        }

        /// <summary>
        /// Initialises an instance of CustomerReservation class.
        /// </summary>
        public CustomerReservation()
        {
            Reservation = new Reservation();
            Customer = new Customer();
        }

        /// <summary>
        /// Checks if Model of both: Customer and Reservation is valid.
        /// </summary>
        public bool IsValid
        {
            get
            {
                if (Reservation.isValid && Customer.isValid)
                    return true;
                else return false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
