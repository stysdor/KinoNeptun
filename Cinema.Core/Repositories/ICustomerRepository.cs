using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Core.Domain;

namespace Cinema.Core.Repositories
{

    /// <summary>
    /// Interface for Customer repository.
    /// </summary>
    public interface ICustomerRepository : IGetDataRepository<Customer>
    {
        Customer GetOrAddByData(Customer customer);
    }
}
