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

        public void Delete(Guid id)
        {
            _sqlContext.Set<TEntity>().Remove(Select(id));
            _sqlContext.SaveChanges();
        }

        public IList<TEntity> Select() =>
            _sqlContext.Set<TEntity>().ToList();

        public TEntity Select(Guid id) =>
            _sqlContext.Set<TEntity>().Find(id);

        public User SelectByEmail(string email) =>
       _sqlContext.Set<User>().FirstOrDefault(user => user.Email == email);

    }
}
