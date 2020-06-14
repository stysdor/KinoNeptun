using Cinema.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Repositories
{
    public interface IUserRepository
    {
        bool ValidateUser(string userName, string password);
        User GetUser(string userName);
    }
}
