using FluentValidation;
using Modelo.Domain.Entities;

namespace Modelo.Domain.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        TOutputModel Add<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class;

        void Delete(Guid id);

        IEnumerable<TOutputModel> Get<TOutputModel>() where TOutputModel : class;

        TOutputModel GetById<TOutputModel>(Guid id) where TOutputModel : class;
        TOutputModel GetByEmail<TOutputModel>(string email) where TOutputModel : class;

        TOutputModel Update<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class;
    }
}
