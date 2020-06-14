using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Core.Domain;

namespace Cinema.Core.Repositories
{

    /// <summary>
    /// Base interface for other interface repository only with getting data.
    /// </summary>
    public interface IGetDataRepository<T> where T:EntityBase
    {
        T Get(int id);
        IList<T> GetAll();
    }
}
