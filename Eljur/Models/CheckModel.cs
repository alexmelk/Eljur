using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eljur.Models
{
    public class CheckModel
    {
        public int SemesterId { get; set; }
        public List<DateTime> Dates { get; set; }
        public List<string> Texts { get; set; }
    }
}
