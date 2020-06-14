using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Dto
{
    /// <summary>
    /// DTO for Movie.
    /// </summary>
    public class MovieDto
    {
        public int Id { get; set; }
        public string MovieTitle { get; set; }
        public string MovieDescription { get; set; }
        public string CategoryName { get; set; }
        public string Country { get; set; }
        public string YearOfProduction { get; set; }

    }
}
