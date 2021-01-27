using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eljur.Context.Tables
{
    public enum AttestationEnum
    {
        [Display(Name = "Зачёт")]
        Attestation,
        [Display(Name = "Диф. Зачёт")]
        DifferentialAttestation,
        [Display(Name = "Экзамен")]
        Exam
    }
}
