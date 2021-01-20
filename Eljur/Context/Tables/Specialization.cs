using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eljur.Context.Tables
{
    public class Specialization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public List<Group> Groups { get; set; }
    }
}
    