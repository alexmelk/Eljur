using Eljur.Context.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eljur.EF.Custom.Entities
{
    /// <summary>
    /// график контрольных мероприятий по самостоятельной работе
    /// </summary>
    public class GrafikForSr
    {
        public int Id { get; set; }
        public List<int> indepWorkEnums { get; set; }

        [Required]
        public Subject Subject;
        public int SubjectId;
     }
}
