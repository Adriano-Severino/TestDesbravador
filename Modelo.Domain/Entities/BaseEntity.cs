using Modelo.Domain.Entities;

namespace Modelo.Domain.Entities
{
    public abstract class BaseEntity
    {
        public virtual Guid Id { get; set; }
    }
}
