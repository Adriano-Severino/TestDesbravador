using Modelo.Domain.Entities;

namespace Modelo.Domain.Interfaces
{
    public interface IUpdateRepository
    {
        public Task<bool> UpdateStatusProjectAsync(IEnumerable<Project> project);
        public Task<List<Project>> GetProjectAsync();
    }
}
