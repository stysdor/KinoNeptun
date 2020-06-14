using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Domain
{
    /// <summary>
    /// Represents Category table from datebase.
    /// </summary>
    public class Category : EntityBase
    {
        public string CategoryName { get; set; }
    }
}
