using AutoMapper;
using FluentValidation;
using Modelo.Domain.Entities;
using Modelo.Domain.Interfaces;

namespace Modelo.Service.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        private readonly IBaseRepository<TEntity> _baseRepository;
        private readonly IUpdateService _updateService;
        private readonly IMapper _mapper;
        public BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper, IUpdateService updateService)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
            _updateService = updateService;
        }
        public async Task<TOutputModel> AddAsync<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class
        {
            TEntity entity = _mapper.Map<TEntity>(inputModel);
            Validate(entity, Activator.CreateInstance<TValidator>());
            _baseRepository.InsertAsync(entity);
            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);
            return await Task.FromResult(outputModel);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (typeof(TEntity) == typeof(Project))
            {
                var result = await GetByIdAsync<Project>(id);

                if (result == null) { return false; }
                if (result.StatusProjectEnum != Domain.Enums.StatusProjectEnum.Started || result.StatusProjectEnum != Domain.Enums.StatusProjectEnum.Closed || result.StatusProjectEnum != Domain.Enums.StatusProjectEnum.InProgress)
                {
                    await _baseRepository.DeleteAsync(id);
                    return true;
                }
                return false;
            }
            else
            {
                await _baseRepository.DeleteAsync(id);
                return true;
            }
        }
        public async Task<IEnumerable<TOutputModel>> GetAsync<TOutputModel>() where TOutputModel : class
        {
            if (typeof(TEntity) == typeof(Project))
            {
                await _updateService.UpdateStatusProjectAsync();
            }

            var entities = await _baseRepository.SelectAsync();
            var outputModels = entities.Select(s => _mapper.Map<TOutputModel>(s));
            return await Task.FromResult(outputModels);


        }

        public async Task<TOutputModel> GetByIdAsync<TOutputModel>(Guid id) where TOutputModel : class
        {
            var entity = await _baseRepository.SelectAsync(id);
            var outputModel = _mapper.Map<TOutputModel>(entity);
            return await Task.FromResult(outputModel);
        }
        public async Task<TOutputModel> GetByEmailAsync<TOutputModel>(string email) where TOutputModel : class
        {
            var entity = await _baseRepository.SelectByEmailAsync(email);
            var outputModel = _mapper.Map<TOutputModel>(entity);
            return await Task.FromResult(outputModel);
        }

        public async Task<TOutputModel> UpdateAsync<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class
        {
            TEntity entity = _mapper.Map<TEntity>(inputModel);
            Validate(entity, Activator.CreateInstance<TValidator>());
            _baseRepository.UpdateAsync(entity);
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


