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

        public Specialization Specialization { get; set; }

        public List<Student> Students { get; set; }
        public List<Semester> Semesters { get; set; }
    }
}
    