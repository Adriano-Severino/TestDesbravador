using Modelo.Domain.Enums;

namespace Modelo.Domain.Dto
{
    public class CreateProjectDto
    {
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ProjectRiskEnum ProjectRiskEnum { get; set; }
        public StatusProjectEnum StatusProjectEnum { get; set; }
    }
}
