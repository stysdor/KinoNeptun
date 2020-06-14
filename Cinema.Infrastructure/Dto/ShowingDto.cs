using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Dto
{
    /// <summary>
    /// DTO for Showing. It has MovieTitle for easier displaying information.
    /// </summary>
    public class ShowingDto
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public int TheatreId { get; set; }
        public DateTime ShowingDateTime { get; set; }
    }
}
