using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eljur.Context.Tables
{
    public class Theme
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public Subject Subject { get; set; }

        public List<Visit> Visits { get; set; }
    }
}
