using System.ComponentModel.DataAnnotations;

namespace Modelo.Domain.Enums
{
    public enum StatusProjectEnum
    {
        [Display(Name = "Em criaçao")]
        Create = 0,

        [Display(Name = "Em análise")]
        UnderAnalysis = 2,

        [Display(Name = "Análise realizada")]
        AnalysisPerformed = 4,

        [Display(Name = "Análise aprovada")]
        AnalysisApproved = 6,

        [Display(Name = "Iniciado")]
        Started = 8,

        [Display(Name = "Planejado")]
        Planned = 10,

        [Display(Name = "Em andamento")]
        InProgress = 12,

        [Display(Name = "Encerrado")]
        Closed = 14,

        [Display(Name = "Cancelado")]
        Canceled = 16
    }
}