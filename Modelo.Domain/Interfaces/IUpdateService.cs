namespace Modelo.Domain.Interfaces
{
    public interface IUpdateService
    {
        public Task<bool> UpdateStatusProjectAsync();
    }
}
