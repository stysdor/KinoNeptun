using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Core.Domain;

namespace Cinema.Core.Repositories
{
    /// <summary>
    /// Interface for Showing repository.
    /// </summary>
    public interface IShowingRepository : IDataRepository<Showing>
    {
        IList<Showing> GetShowingsByDate(DateTime date);
        IList<Showing> GetShowingsByMovie(int movieId);
        IList<Showing> GetActuall(int n);
    }
}
