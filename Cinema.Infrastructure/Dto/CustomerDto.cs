using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Dto
{
    /// <summary>
    /// DTO for Customer.
    /// </summary>
    public class CustomerDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
