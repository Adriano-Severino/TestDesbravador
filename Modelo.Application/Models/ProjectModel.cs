using Modelo.Domain.Enums;

namespace Modelo.Application.Models
{
    public class ProjectModel
    {
        public Guid Id { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public ProjectRiskEnum ProjectRiskEnum { get; set; }
    }
}
