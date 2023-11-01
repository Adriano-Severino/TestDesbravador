using FluentValidation;
using Modelo.Domain.Entities;

namespace Modelo.Domain.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        Task<TOutputModel> Add<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
              where TValidator : AbstractValidator<TEntity>
              where TInputModel : class
              where TOutputModel : class;

        Task<bool> Delete(Guid id);
        Task<IEnumerable<TOutputModel>> Get<TOutputModel>() where TOutputModel : class;

        Task<TOutputModel> GetById<TOutputModel>(Guid id) where TOutputModel : class;
        Task<TOutputModel> GetByEmail<TOutputModel>(string email) where TOutputModel : class;

        Task<TOutputModel> Update<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
              where TValidator : AbstractValidator<TEntity>
              where TInputModel : class
              where TOutputModel : class;
        //void CreateEmployeesByApi(Employees employees);
    }
}
