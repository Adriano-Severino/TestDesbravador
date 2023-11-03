using Modelo.Domain.Enums;
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
            var filtro = project.Where(x => x.StatusProjectEnum != StatusProjectEnum.Canceled).ToList();

            if (filtro.Count > 0)
            {
                foreach (var item in filtro)
                {
                    if ((int)item.StatusProjectEnum < 16)
                    {
                        item.StatusProjectEnum++;

                        if ((int)item.StatusProjectEnum == 16 && item.EndDate == null)
                        {
                            item.EndDate = DateTime.UtcNow;
                        }
                    }

                }
                await _updateRepository.UpdateStatusProjectAsync(filtro);

            }
            return true;

        }
    }
}
