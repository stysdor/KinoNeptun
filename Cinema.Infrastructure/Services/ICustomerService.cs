using Cinema.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Services
{
    /// <summary>
    /// Interface for CustomerService.
    /// </summary>
    public interface ICustomerService
    {
        CustomerDto Get(int id);
        CustomerDto GetByData(CustomerDto customerInput);
        CustomerDto GetOrAddByData(CustomerDto customerInput);
    }
}
