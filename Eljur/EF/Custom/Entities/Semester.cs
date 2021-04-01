using Eljur.EF.Custom.Entities;
using System.Collections.Generic;

namespace Eljur.Context.Tables
{
    public class Semester
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public virtual Group Group { get; set; }
        public int GroupId { get; set; }
        public virtual List<Subject> Subjects { get; set; }
        public virtual List<GroupVisit> GroupVisits { get; set; }
        public virtual List<SemesterStudent> SemesterStudents { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Semester> Semesters { get; set; }
        public virtual List<Check> Checks { get; set; }
    }
}