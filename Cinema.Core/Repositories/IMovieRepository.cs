using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Core.Domain;

namespace Cinema.Core.Repositories
{

    /// <summary>
    /// Interface for Movie repository.
    /// </summary>
    public interface IMovieRepository : IDataRepository <Movie>
    {
        IList<Movie> GetMoviesByCategory(Category category);
    }
}
