using AutoMapper;
using Cinema.Core.Repositories;
using Cinema.Infrastructure.Dto;
using Cinema.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Services
{
    public class UserService: IUserService
    {
        private IMapper _mapper;
        private IUserRepository _userRepository;
        public UserService(IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = new UserRepository();
        }

        public bool ValidateUser(string userName, string password)
        {
            return _userRepository.ValidateUser(userName, password);
        }
        public UserDto GetUser(string userName)
        {
            return _mapper.Map<UserDto>(_userRepository.GetUser(userName));
        }

    }
}
