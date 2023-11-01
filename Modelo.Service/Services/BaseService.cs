using AutoMapper;
using FluentValidation;
using Modelo.Domain.Entities;
using Modelo.Domain.Interfaces;

namespace Modelo.Service.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        private readonly IBaseRepository<TEntity> _baseRepository;
        private readonly IMapper _mapper;
        //private readonly IServiceProvider _serviceProvider;
        public BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper/*, IServiceProvider serviceProvider*/)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
            //_serviceProvider = serviceProvider;
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
            var result = await GetById<Project>(id);

            if (result == null) { return false; }
            if (result.StatusProjectEnum != Domain.Enums.StatusProjectEnum.Started || result.StatusProjectEnum != Domain.Enums.StatusProjectEnum.Closed || result.StatusProjectEnum != Domain.Enums.StatusProjectEnum.InProgress) 
            {
                await _baseRepository.Delete(id);
                return true;
            }
            return false;
           
        }
        public async Task<IEnumerable<TOutputModel>> Get<TOutputModel>() where TOutputModel : class
        {
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
        //private void UpdateProjectStatus(object state)
        //{
        //    using (var scope = _serviceProvider.CreateScope())
        //    {
        //        var baseRepository = scope.ServiceProvider.GetRequiredService<IBaseRepository<TEntity>>();
        //        var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
        //        var entities = baseRepository.Select().Result;
        //        var outputModels = entities.Select(s => mapper.Map<TEntity>(s));

        //        // Resto do seu código...
        //    }

        //}

        //TODO: A cada instante, o projeto deve estar em um status específico e único. Os status possíveis não são cadastrados no sistema e são: em análise, análise realizada, análise aprovada, iniciado,
    }
}

