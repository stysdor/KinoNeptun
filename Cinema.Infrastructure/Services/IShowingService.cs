using Cinema.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Services
{
    /// <summary>
    /// Interface for ShowingService.
    /// </summary>
    public interface IShowingService
    {
        ShowingDto Get(int id);
        IList<ShowingDto> GetAll();
        IList<ShowingDto> GetActuall(int n);
        IList<ShowingDto> GetByMovie(int id);
        int InsertOrUpdate(ShowingDto item);
        void Remove(int id);
    }
}
