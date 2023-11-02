using Modelo.Domain.Entities;

namespace Modelo.Domain.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        void Insert(TEntity obj);
        void Update(TEntity obj);
        Task<bool> Delete(Guid id);
        Task<IList<TEntity>> Select();
        Task<TEntity> Select(Guid id);
        Task<Employees> SelectByEmail(string email);

    }
}
