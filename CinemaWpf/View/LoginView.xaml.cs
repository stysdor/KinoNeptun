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
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private static LoginView _this;

        /// <summary>
        /// Gets an object of LoginView.
        /// </summary>
        /// <returns></returns>
        public static LoginView GetInstance()
        {
            return _this;
        }

        public LoginView()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
            _this = this;
        }

        public LoginView(User model)
        {
            InitializeComponent();
            DataContext = new LoginViewModel(model);
            _this = this;
        }
    }
}
