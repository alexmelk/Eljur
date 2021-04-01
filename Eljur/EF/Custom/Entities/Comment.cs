using Eljur.Context.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Eljur.Context.Tables
{
    public class Comment
    {
        public int Id { get; set; }

        public string DekanDescription { get; set; }
        public string TeacherDescription { get; set; }
        [Required]
        public virtual Semester Semester { get; set; }
        public int SemesterId { get; set; }
    }
}
