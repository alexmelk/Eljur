﻿using System;
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
        public double AllowedHours { get; set; }

        public TypeSubjectEnum Type { get; set; }
        [Required]
        public Subject Subject { get; set; }
        public int SubjectId { get; set; }

        public ThemeGroup ThemeGroup { get; set; }
        public int ThemeGroupId { get; set; }

        public List<ThemeVisit> ThemeVisits { get; set; }
        public List<StudentVisit> StudentVisits { get; set; }
        public List<GroupVisit> GroupVisits { get; set; }
    }
}
