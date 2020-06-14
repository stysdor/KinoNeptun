using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Domain
{
    /// <summary>
    /// Base class for every class represents table from datebase.
    /// </summary>
    public class EntityBase
    {
        public int Id { get; set; }
    }
}
