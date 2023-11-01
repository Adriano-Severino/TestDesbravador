using Modelo.Domain.Enums;

namespace Modelo.Application.Models
{
    public class CreateProjectModel
    {
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public ProjectRiskEnum ProjectRiskEnum { get; set; }
    }
}
