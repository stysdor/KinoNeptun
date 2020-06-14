using CinemaWpf.Commands;
using CinemaWpf.Model;
using CinemaWpf.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaWpf.ViewModel
{
    public class TicketViewModel : TicketReservationViewModel
    {
        public TicketViewModel(CustomerReservation model) : base(model)
        {
            SaveCommand = new SaveReservationCommand(this);
            Reservation.Customer.CustomerName = "Unknown";
            Reservation.Customer.CustomerSurname = "Unknown";
            Reservation.Reservation.Status = 1;
        }

        public override void saveReservation()
        {
            bool isSend = false;
            isSend = Controller.Post(Reservation);
            if (isSend)
                TicketView.GetInstance().Close();
        }
    }
}
