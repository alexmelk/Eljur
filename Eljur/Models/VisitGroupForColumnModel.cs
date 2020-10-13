using Eljur.Context.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eljur.Models
{
    public class VisitGroupForColumnModel
    {
        public int ThemeId { get; set; }

        public TypeSubjectEnum TypeSubject { get; set; }
        public List<VisitModify> VisitsModify { get; set; }
    }
}
