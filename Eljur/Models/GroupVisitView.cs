using Eljur.Context.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eljur.Models
{
    public class GroupVisitView
    {
        public int GroupId { get; set; }
        public int SubjectId { get; set; }
        public int Semester { get; set; }
        public List<GroupVisit> Visits { get; set; }
        public Filter Filter{get;set;}
     
    }
}
