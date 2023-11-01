using Modelo.Domain.Entities;
using Modelo.Domain.Enums;

namespace Modelo.Domain.Entities
{
    public class Project : BaseEntity
    {
        public Project()
        {
            StartDate = new DateTime();
            Employees = new List<Employees>();
        }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ProjectRiskEnum ProjectRiskEnum { get; set; }
        public StatusProjectEnum StatusProjectEnum { get; set; }

        public virtual ICollection<Employees> Employees { get; set; }
    }
}