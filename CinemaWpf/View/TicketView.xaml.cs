using CinemaWpf.Model;
using CinemaWpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CinemaWpf.View
{
    /// <summary>
    /// Interaction logic for TicketView.xaml
    /// </summary>
    public partial class TicketView : Window
    {
        private static TicketView _this;

        /// <summary>
        /// Gets an object of TicketView.
        /// </summary>
        /// <returns></returns>
        public static TicketView GetInstance()
        {
            return _this;
        }

        /// <summary>
        /// Initialize an instance of TicketView class.
        /// </summary>
        public TicketView()
        {
            InitializeComponent();
            _this = this;
        }

        /// <summary>
        /// Initialize an instance of TicketView class.
        /// </summary>
        /// <param name="model">Model of CustomerReservation Class.</param>
        public TicketView(CustomerReservation model)
        {
            InitializeComponent();
            DataContext = new TicketViewModel(model);
            _this = this;
        }
    }
}
