using System.ComponentModel.DataAnnotations;

namespace Eljur.Context.Tables
{
    public enum TypeSubjectEnum
    {
        [Display(Name = "Лб")]
        lb,
        [Display(Name = "Лк")]
        lk,
        [Display(Name = "Пр")]
        pr
    }
}