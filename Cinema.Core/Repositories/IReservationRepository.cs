using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Core.Domain;

namespace Cinema.Core.Repositories
{

    /// <summary>
    /// Interface for Reservation repository.
    /// </summary>
    public interface IReservationRepository : IDataRepository<Reservation>
    {
        IList<Reservation> GetReservationByCustomer(Customer customer);
        IList<Reservation> GetReservationByShowing(Showing showing);

    }
}
