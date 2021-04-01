using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eljur.Context.Tables
{
    public class Student
    {
        public int Id { get; set; }

        public string FIO { get; set; }
        [Required]
        public virtual Group Group { get; set; }
        public int GroupId { get; set; }

        public virtual List<StudentVisit> StudentVisits { get; set; }

    }
}
