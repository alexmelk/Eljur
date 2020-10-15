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

        [System.ComponentModel.DataAnnotations.Required]
        public Group Group { get; set; }

        public List<StudentVisit> StudentVisits { get; set; }

    }
}
