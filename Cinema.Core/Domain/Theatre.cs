using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Domain
{
    /// <summary>
    /// Represents Theatre table from datebase.
    /// </summary>
    public class Theatre :EntityBase
    {
        public string TheatreName { get; set; }
    }
}
