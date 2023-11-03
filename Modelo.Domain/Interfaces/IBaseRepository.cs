using Modelo.Domain.Entities;

namespace Modelo.Domain.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        public void InsertAsync(TEntity obj);
        public void UpdateAsync(TEntity obj);
        public Task<bool> DeleteAsync(Guid id);
        public Task<IList<TEntity>> SelectAsync();
        public Task<TEntity> SelectAsync(Guid id);
        public Task<Employees> SelectByEmailAsync(string email);

    }
}
