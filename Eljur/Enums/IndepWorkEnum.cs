using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eljur.Context.Tables
{
    public enum IndepWorkEnumRus
    {
        [Display(Name = "Отсутствует")]
        нет,
        [Display(Name = "ИЗ-индивидуальное задание")]
        ИЗ,
        [Display(Name = "Кр-контрольная работа")]
        Кр,
        [Display(Name = "КР-курсовая работа")]
        КР,
        [Display(Name = "КП-курсовай проект")]
        КП,
        [Display(Name = "РГР-расчетно-графическая работа")]
        РГР,
        [Display(Name = "Р-реферат")]
        Р,
        [Display(Name = "Тт-текущее тестирование")]
        Тт,
        [Display(Name = "Тр-рубежное тестирование")]
        Тр,
    }
}
