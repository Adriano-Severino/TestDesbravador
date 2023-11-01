using System.ComponentModel.DataAnnotations;

namespace Modelo.Domain.Enums
{
    public enum ProjectRiskEnum
    {
        [Display(Name = "Baixo risco")]
        LowRisk,

        [Display(Name = "Médio risco")]
        MediumRisk,

        [Display(Name = "Alto risco")]
        HighRisk,
    }
}
