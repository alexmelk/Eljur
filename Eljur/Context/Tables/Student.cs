using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eljur.Context.Tables
{
    public class Student
    {
        public int Id { get; set; }

        public string FIO { get; set; }

        public Group Group { get; set; }
        public List<Visit> Visits { get; set; }

    }
}
