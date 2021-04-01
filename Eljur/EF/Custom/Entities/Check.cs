using Eljur.Context.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eljur.Context.Tables
{
    public class Check
    {
        public int Id { get; set; }

        public DateTime Date { get;  set; }
        public string Text { get; set; }

        public virtual Semester Semester { get; set; }
        public int SemesterId { get; set; }
    }
}
