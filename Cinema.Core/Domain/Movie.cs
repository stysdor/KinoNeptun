using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Domain
{
    /// <summary>
    /// Represents Movie table from datebase.
    /// </summary>
    public class Movie : EntityBase
    {
        public string MovieTitle { get; set; }
        public string MovieDescription { get; set; }
        public Category CategoryId { get; set; }
        public string Country { get; set; }
        public string YearOfProduction { get; set; }

    }
}
