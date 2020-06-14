using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Infrastructure.Dto;

namespace Cinema.Infrastructure.Services
{
    /// <summary>
    /// Interface for MovieService.
    /// </summary>
    public interface IMovieService
    {
        MovieDto Get(int id);
        IList<MovieDto> GetAll();
        IList<MovieDto> GetByCategory(string name);
        int InsertOrUpdate(MovieDto item);
        void Remove(int id);
    }
}
