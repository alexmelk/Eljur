using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eljur.Context.Tables
{
    public class StudentVisit
    {
        public int Id { get; set; }

        public Student Student { get; set; }
        [Required]
        public GroupVisit GroupVisit { get; set; }
        [Required]
        public Subject Subject { get; set; }
        [Required]
        public Theme Theme { get; set; }
        public TypeVisitEnum TypeVisit { get; set; }
    }
}
