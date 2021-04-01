using Eljur.Context.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eljur.EF.Custom.Entities
{
    public class SemesterStudent
    {
        public int Id { get; set; }

        [Required]
        public virtual Student Student { get; set; }
        public int StudentId { get; set; }
        [Required]
        public virtual Semester Semester { get; set; }
        public int SemesterId{get;set;}
        public string SpecialyMark { get; set; }

    }
}
