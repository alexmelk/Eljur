using Eljur.Context.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eljur.Models
{
    public class ChoosePropertyVisit
    {
        public Group Group { get; set; } = new Group();
        public Subject Subject { get; set; } = new Subject();
        public int Time { get; set; }
    }
}
