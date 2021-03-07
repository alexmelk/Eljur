using Eljur.Context.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eljur.Models
{
    public class DateTaskDoneModel
    {
        public Subject Subject { get; set; }
        public List<DateTime> DateTaskDone { get; set; }
    }
}
