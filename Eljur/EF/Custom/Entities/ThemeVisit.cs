using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eljur.Context.Tables
{
    public class ThemeVisit
    {
        public int Id { get; set; }

        public double HoursPerVisit { get; set; }
        public TypeSubjectEnum TypeSubject { get; set; }
        public Theme Theme { get; set; }
    }
}
