using Eljur.EF.Custom.Entities;
using System.Collections.Generic;

namespace Eljur.Context.Tables
{
    public class Semester
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public Group Group { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<GroupVisit> GroupVisits { get; set; }
        public List<SemesterStudent> SemesterStudents { get; set; }
    }
}