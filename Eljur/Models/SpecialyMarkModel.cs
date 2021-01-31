using Eljur.Context.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eljur.Models
{
    public class SpecialyMarkModel
    {
        public Semester Semester { get; set; }
        public List<Student> Students { get; set; }
        public List<string> SpecialyMark { get; set; }
    }
}
