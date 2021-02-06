using Eljur.Context.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eljur.Models
{
    public class CommentsModel
    {
        public int SemesterId { get; set; }
        public List<string> DekanDescriptions { get; set; }
        public List<string> TeacherDescriptions { get; set; }
    }
}
