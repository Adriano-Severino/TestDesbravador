using FluentValidation;
using Modelo.Domain.Entities;

namespace Modelo.Domain.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        public Task<TOutputModel> AddAsync<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
               where TValidator : AbstractValidator<TEntity>
               where TInputModel : class
               where TOutputModel : class;
        public Task<bool> DeleteAsync(Guid id);
        public Task<IEnumerable<TOutputModel>> GetAsync<TOutputModel>() where TOutputModel : class;
        public Task<TOutputModel> GetByIdAsync<TOutputModel>(Guid id) where TOutputModel : class;
        public Task<TOutputModel> GetByEmailAsync<TOutputModel>(string email) where TOutputModel : class;
        public Task<TOutputModel> UpdateAsync<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
               where TValidator : AbstractValidator<TEntity>
               where TInputModel : class
               where TOutputModel : class;
    }
}
