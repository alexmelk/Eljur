using Eljur.Context.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eljur.Models
{
    public class ChoosePropertyVisit
    {
        public Group Group { get; set; }
        public Subject Subject { get; set; }
        public int Time { get; set; }
    }
}
