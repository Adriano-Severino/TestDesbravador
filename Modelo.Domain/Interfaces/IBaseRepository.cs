using Modelo.Domain.Entities;

namespace Modelo.Domain.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        void Insert(TEntity obj);
        void Update(TEntity obj);
        void Delete(Guid id);
        IList<TEntity> Select();
        TEntity Select(Guid id);
        User SelectByEmail(string email);
    }
}
