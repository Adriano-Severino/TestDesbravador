using Modelo.Domain.Interfaces;

namespace Modelo.Service.Services
{
    public class UpdateService : IUpdateService
    {
        private readonly IUpdateRepository _updateRepository;
        public UpdateService(IUpdateRepository updateRepository)
        {
            _updateRepository = updateRepository;
        }
        public async Task<bool> UpdateStatusProjectAsync()
        {
            var project = await _updateRepository.GetProjectAsync();
            foreach (var item in project)
            {
                if ((int)item.StatusProjectEnum < 16)
                {
                    item.StatusProjectEnum++;
                }

            }
            await _updateRepository.UpdateStatusProjectAsync(project);

            return true;

        }
    }
}
