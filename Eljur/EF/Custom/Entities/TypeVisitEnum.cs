using System.ComponentModel.DataAnnotations;

namespace Eljur.Context.Tables
{
    public enum TypeVisitEnum
    {
        [Display(Name="Присутствует")]
        Present,
        [Display(Name = "Отсутствует")]
        Absent,
        [Display(Name = "Отсутствует по ув.")]
        ValidAbsent
    }
}