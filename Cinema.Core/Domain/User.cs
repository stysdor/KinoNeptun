using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Domain
{
    /// <summary>
    /// Represents User table from datebase.
    /// </summary>
    public class User :EntityBase
    {
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
    }
}
