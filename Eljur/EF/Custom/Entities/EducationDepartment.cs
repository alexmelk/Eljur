﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eljur.Context.Tables
{
    /// <summary>
    ///  Отделение [очное, заочное]
    /// </summary>
    public class EducationDepartment
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<EducationLevel> EducationLevels { get; set; }
        public virtual List<Specialization> Specializations { get; set; }
    }
}
