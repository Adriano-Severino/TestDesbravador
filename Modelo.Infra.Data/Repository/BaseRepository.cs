using Microsoft.EntityFrameworkCore;
using Modelo.Domain.Entities;
using Modelo.Domain.Interfaces;
using Modelo.Infra.Data.Context;

namespace Modelo.Infra.Data.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly SqlContext _sqlContext;

        public BaseRepository(SqlContext sqlContext)
        {
            _sqlContext = sqlContext;
        }
        public void Insert(TEntity obj)
        {
            _sqlContext.Set<TEntity>().Add(obj);
            _sqlContext.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            _sqlContext.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _sqlContext.SaveChanges();
        }

        public async Task<bool> Delete(Guid id)
        {
            _sqlContext.Set<TEntity>().Remove(Select(id).Result);
            await _sqlContext.SaveChangesAsync();
            return true;
        }

        public async Task<IList<TEntity>> Select()
        {
            if (typeof(TEntity) == typeof(Project))
            {
                return await _sqlContext.Set<TEntity>()
                    .Include("Employees")
                    .ToListAsync();
            }
            else
            {
                return await _sqlContext.Set<TEntity>().ToListAsync();
            }
        }

        public async Task<TEntity> Select(Guid id) =>
          await _sqlContext.Set<TEntity>().FindAsync(id);

        public async Task<Employees> SelectByEmail(string email) =>
          await _sqlContext.Set<Employees>().FirstOrDefaultAsync(user => user.Email == email);

    }
}
