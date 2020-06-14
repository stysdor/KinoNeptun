using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Core.Domain;

namespace Cinema.Core.Repositories
{
    /// <summary>
    /// Base interface for other interface repository with getting and setting data.
    /// </summary>
    public interface IDataRepository<T>  where T:EntityBase 
    {
        T Get(int id);
        IList<T> GetAll();
        int InsertOrUpdate(T item);
        void Remove(int id);
    }
}
