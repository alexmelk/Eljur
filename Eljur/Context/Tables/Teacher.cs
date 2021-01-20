using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eljur.Context.Tables
{
    public class Teacher
    {
        public int Id { get; set; }
        public string FIO { get; set; }

        public List<Subject> Subjects { get; set; }
    }
}
