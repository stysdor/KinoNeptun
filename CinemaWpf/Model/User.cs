using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaWpf.Model
{
    public class User: IDataErrorInfo
    {
        

        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public string Error
        {
            get { return null; }
        }

        public string this[string propertyName]
        {
            get
            {
                return GetValidationError(propertyName);
            }
        }

        public readonly string[] ValidateProperties =
      {
            "Login",
            "Password"
        };

        public bool isValid
        {
            get
            {
                foreach (string property in ValidateProperties)
                    if (GetValidationError(property) != null)
                        return false;
                return true;
            }
        }

        string GetValidationError(string propertyName)
        {
            string error = null;
            switch (propertyName)
            {
                case "Login":
                    error = ValidateLogin();
                    break;
                case "Password":
                    error = ValidatePassword();
                    break;
            }

            return error;
        }

        private string ValidateLogin()
        {
            if (Login == null)
            {
                return "Podaj nazwę użytkownika";
            }
            else
            {
                return null;
            }
        }

        private string ValidatePassword()
        {
            if (Password == null)
            {
                return "Podaj hasło";
            }
            else
            {
                return null;
            }
        }
    }
}
