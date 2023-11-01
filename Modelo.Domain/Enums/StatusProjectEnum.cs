using System.ComponentModel.DataAnnotations;

namespace Modelo.Domain.Enums
{
    public enum StatusProjectEnum
    {
        [Display(Name = "Em análise")]
        UnderAnalysis,

        [Display(Name = "Análise realizada")]
        AnalysisPerformed,

        [Display(Name = "Análise aprovada")]
        AnalysisApproved,

        [Display(Name = "Iniciado")]
        Started,

        [Display(Name = "Planejado")]
        Planned,

        [Display(Name = "Em andamento")]
        InProgress,

        [Display(Name = "Encerrado")]
        Closed,

        [Display(Name = "Cancelado")]
        Canceled
    }
}