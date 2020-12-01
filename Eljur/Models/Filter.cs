using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eljur.Models
{
    public class Filter
    {
        public string LastName { get; set; }
        public DateTime Before { get; set; }

        public DateTime From { get; set; }
    }
}
