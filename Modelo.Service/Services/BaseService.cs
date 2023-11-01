using AutoMapper;
using FluentValidation;
using Modelo.Domain.Entities;
using Modelo.Domain.Interfaces;
using Modelo.Infra.CrossCutting.Interfaces;

namespace Modelo.Service.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        private readonly IBaseRepository<TEntity> _baseRepository;
        private readonly IMapper _mapper;
        private readonly IEmployeesServiceApi _EmployeesApi;

        public BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper, IEmployeesServiceApi employees)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
            _EmployeesApi = employees;
        }
        public async Task<TOutputModel> Add<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class
        {
            TEntity entity = _mapper.Map<TEntity>(inputModel);
            Validate(entity, Activator.CreateInstance<TValidator>());
            _baseRepository.Insert(entity);
            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);
            return await Task.FromResult(outputModel);
        }

        public async Task<bool> Delete(Guid id) => await _baseRepository.Delete(id);

        public async Task<IEnumerable<TOutputModel>> Get<TOutputModel>() where TOutputModel : class
        {
            var entities = await _baseRepository.Select();
            var outputModels = entities.Select(s => _mapper.Map<TOutputModel>(s));
            return await Task.FromResult(outputModels);
        }

        public async Task<TOutputModel> GetById<TOutputModel>(Guid id) where TOutputModel : class
        {
            var entity = _baseRepository.Select(id);
            var outputModel = _mapper.Map<TOutputModel>(entity);
            return await Task.FromResult(outputModel);
        }
        public async Task<TOutputModel> GetByEmail<TOutputModel>(string email) where TOutputModel : class
        {
            var entity = _baseRepository.SelectByEmail(email);
            var outputModel = _mapper.Map<TOutputModel>(entity);
            return await Task.FromResult(outputModel);
        }

        public async Task<TOutputModel> Update<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class
        {
            TEntity entity = _mapper.Map<TEntity>(inputModel);
            Validate(entity, Activator.CreateInstance<TValidator>());
            _baseRepository.Update(entity);
            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);

            return await Task.FromResult(outputModel);
        }

        private void Validate(TEntity obj, AbstractValidator<TEntity> validator)
        {
            if (obj == null)
                throw new Exception("Registros não detectados!");
            validator.ValidateAndThrow(obj);
        }
    }
}