using System.ComponentModel.DataAnnotations;

namespace Eljur.Context.Tables
{
    public enum TypeSubjectEnum
    {
        [Display(Name = "Лр")]
        lb,
        [Display(Name = "Л")]
        lk,
        [Display(Name = "Пз")]
        pr
    }
}