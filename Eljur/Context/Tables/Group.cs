using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eljur.Context.Tables
{
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Student> Students { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<Visit> Visits { get; set; }
    }
}
