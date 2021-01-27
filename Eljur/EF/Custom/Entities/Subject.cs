using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eljur.Context.Tables
{
    public class Subject
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Semester Semester { get; set; }
        public Group Group { get; set; }
        public Teacher Teacher { get; set; }
        public AttestationEnum Attestation { get; set; }
        public double AttestationHours { get; set; }

        public List<Theme> Themes { get; set; }
    }
}
