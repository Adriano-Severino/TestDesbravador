using Modelo.Domain.Enums;

namespace Modelo.Application.Models
{
    public class ProjectModel
    {
        public Guid Id { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ProjectRiskEnum ProjectRiskEnum { get; set; }
        public StatusProjectEnum StatusProjectEnum { get; set; }
    }
}
