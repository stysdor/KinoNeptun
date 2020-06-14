using CinemaWpf.Commands;
using CinemaWpf.Controllers;
using CinemaWpf.Model;
using CinemaWpf.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CinemaWpf.ViewModel
{
    public class LoginViewModel 
    {
        private User user;
        public bool isValidatedUser;
        public bool isChecked;

        public string ErrorLoginMessage
        {
            get
            {
                if (!isValidatedUser && isChecked)
                    return "Nieprawidłowe dane logowania.";
                else
                    return null;
            }
        }

        public AccountController Controller { get; set; }

        /// <summary>
        /// Initalize a new instance of LoginViewModel class
        /// </summary>
        public LoginViewModel()
        {
            User = new User();
            LoginCommand = new LoginCommand(this);
            Controller = new AccountController();
            isValidatedUser = false;
            isChecked = false;
        }
        /// <summary>
        /// Initalize a new instance of LoginViewModel class
        /// </summary>
        public LoginViewModel(User model)
        {
            User = model;
            LoginCommand = new LoginCommand(this);
            Controller = new AccountController();
        }

        public ICommand LoginCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the User instance
        /// </summary>
        public User User
        {
            get { return user; }
            set { user = value;  }
        }

        public void Login()
        {
            if (User.isValid)
            {
                isValidatedUser = Controller.Login(User);
                isChecked = true;
            }
            if (isValidatedUser)
            {
                MainWindow view = new MainWindow();
                LoginView.GetInstance().Close();
                view.ShowDialog();
            }
            else
                MessageBox.Show(ErrorLoginMessage, "Info", MessageBoxButton.OK, MessageBoxImage.Error);

        }
    }
}
