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

        public virtual Student Student { get; set; }
        public int StudentId { get; set; }
        [Required]
        public virtual GroupVisit GroupVisit { get; set; }
        public int GroupVisitId { get; set; }
        [Required]
        public virtual Subject Subject { get; set; }
        public int SubjectId { get; set; }
        [Required]
        public virtual Theme Theme { get; set; }
        public int ThemeId { get; set; }
        public TypeVisitEnum TypeVisit { get; set; }
    }
}
