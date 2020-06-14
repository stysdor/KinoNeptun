using Cinema.Infrastructure.Dto;
using Cinema.Infrastructure.Mappers;
using Cinema.Infrastructure.Services;
using CinemaWpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaWpf.Controllers
{
    /// <summary>
    /// This class is responsible for delegate request to UserService from Infrustructure Layer .
    /// </summary>
    public class AccountController
    {
        private IUserService _userService;

        /// <summary>
        /// Initialize a new instance of AccountController class.
        /// </summary>
        public AccountController()
        {
            _userService = new UserService(AutoMapperConfig.Initialize());
        }

        /// <summary>
        /// Login the user.
        /// </summary>
        /// <param name="model">Instance of User</param>
        /// <returns>Returns true if operation succeed.</returns>
        public bool Login(User model)
        {
            {
                UserDto user = new UserDto()
                {
                   Login = model.Login,
                   Password = model.Password
                };
                var result = _userService.ValidateUser(user.Login, user.Password);

                return result;
            }
        }
    }
}
