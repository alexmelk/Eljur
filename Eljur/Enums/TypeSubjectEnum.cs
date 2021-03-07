using System.ComponentModel.DataAnnotations;

namespace Eljur.Context.Tables
{
    public enum TypeSubjectEnum
    {
        [Display(Name = "ЛР")]
        lb,
        [Display(Name = "Л")]
        lk,
        [Display(Name = "ПЗ")]
        pr,
        [Display(Name = "КСР")]
        ksr
    }
}