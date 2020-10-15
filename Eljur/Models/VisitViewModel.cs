using Eljur.Context.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eljur.Models
{
    public class VisitViewModel
    {
        public DateTime Date { get; set; }
        public int SubjectId { get; set; }
        public int GroupId { get; set; }

        public ChoosePropertyVisit Input { get; set; }
        public List<VisitGroupForColumnModel> Output { get; set; }
    }
}
