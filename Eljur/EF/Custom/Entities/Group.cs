using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eljur.Context.Tables
{
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        public virtual Specialization Specialization { get; set; }
        public int SpecializationId { get; set; }

        public virtual List<Student> Students { get; set; }
        public virtual List<Semester> Semesters { get; set; }
    }
}
    