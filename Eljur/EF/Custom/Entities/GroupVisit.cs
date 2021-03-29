using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eljur.Context.Tables
{
    public class GroupVisit
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        [Required]
        public Group Group { get; set; }
        public int GroupId { get; set; }
        [Required]
        public Subject Subject { get; set; }
        public int SubjectId { get; set; }
        [Required]
        public Semester Semester { get; set; }
        public int SemesterId { get; set; }
        [Required]
        public List<ThemeVisit> ThemeVisits { get; set; }
        [Required]
        public List<StudentVisit> StudentVisits { get; set; }
        [Required]
        public Theme Theme { get; set; }
        public int ThemeId { get; set; }

    }
}
