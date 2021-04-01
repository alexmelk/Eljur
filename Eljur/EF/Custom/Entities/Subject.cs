using Eljur.EF.Custom.Entities;
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

        public virtual Semester Semester { get; set; }
        public int SemesterId { get; set; }
        public virtual Group Group { get; set; }
        public int GroupId { get; set; }
        public virtual Teacher Teacher { get; set; }
        public int TeacherId { get; set; }

        public AttestationEnum Attestation { get; set; }
        public double AttestationHours { get; set; }

        public virtual GrafikForSr? GrafikForSr { get; set; }
        public int? GrafikForSrId { get; set; }

        public virtual List<DateTime> DateTaskDone { get; set; }
        public virtual List<Theme> Themes { get; set; }
    }
}
