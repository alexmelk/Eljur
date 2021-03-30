using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eljur.Context.Tables
{
    public class Specialization
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public EducationDepartment EducationDepartment { get; set; }
        public int EducationDepartmentId { get; set; }
        public EducationLevel EducationLevel { get; set; }
        public int EducationLevelId { get; set; }

        public List<Group> Groups { get; set; }
    }
}
    