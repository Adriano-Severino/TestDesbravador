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

        public async Task<bool> Delete(Guid id)
        {
            if (typeof(TEntity) == typeof(Project))
            {
                var result = await GetById<Project>(id);

                if (result == null) { return false; }
                if (result.StatusProjectEnum != Domain.Enums.StatusProjectEnum.Started || result.StatusProjectEnum != Domain.Enums.StatusProjectEnum.Closed || result.StatusProjectEnum != Domain.Enums.StatusProjectEnum.InProgress)
                {
                    await _baseRepository.Delete(id);
                    return true;
                }
                return false;
            }
            else
            {
                await _baseRepository.Delete(id);
                return true;
            }
        }
        public async Task<IEnumerable<TOutputModel>> Get<TOutputModel>() where TOutputModel : class
        {
            if (typeof(TEntity) == typeof(Project))
            {
                await _updateService.UpdateStatusProjectAsync();
            }

            var entities = await _baseRepository.Select();
            var outputModels = entities.Select(s => _mapper.Map<TOutputModel>(s));
            return await Task.FromResult(outputModels);


        }

        public async Task<TOutputModel> GetById<TOutputModel>(Guid id) where TOutputModel : class
        {
            var entity = await _baseRepository.Select(id);
            var outputModel = _mapper.Map<TOutputModel>(entity);
            return await Task.FromResult(outputModel);
        }
        public async Task<TOutputModel> GetByEmail<TOutputModel>(string email) where TOutputModel : class
        {
            var entity = await _baseRepository.SelectByEmail(email);
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


