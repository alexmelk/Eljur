using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eljur.Context.Tables
{
    public class Visit
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public TypeVisitEnum TypeVisit { get; set; }
        public TypeSubjectEnum TypeSubject { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public Subject Subject { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public Theme Theme { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public Student Student { get;set;}

    }
}
