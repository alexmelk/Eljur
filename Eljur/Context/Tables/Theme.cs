using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eljur.Context.Tables
{
    public class Theme
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public TypeSubjectEnum Type { get; set; }
        [Required]
        public Subject Subject { get; set; }
        public List<GroupVisit> Visits { get; set; }
        public bool IsChoosen { get; set; }
        public int ChoosenHours { get; set; }
    }
}
