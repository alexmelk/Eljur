using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eljur.Context.Tables
{
    /// <summary>
    /// Уровень образования [бакалавр, спец..]
    /// </summary>
    public class EducationLevel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<EducationDepartment> EducationDepartments { get; set; }
    }
}
