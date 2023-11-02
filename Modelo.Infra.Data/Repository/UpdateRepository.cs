using Microsoft.EntityFrameworkCore;
using Modelo.Domain.Entities;
using Modelo.Domain.Interfaces;
using Modelo.Infra.Data.Context;

namespace Modelo.Infra.Data.Repository
{
    public class UpdateRepository : IUpdateRepository
    {
        private readonly SqlContext _sqlContext;
        public UpdateRepository(SqlContext sqlContext)
        {
            _sqlContext = sqlContext;
        }
        public async Task<bool> UpdateStatusProjectAsync(IEnumerable<Project> project)
        {
            foreach (var proj in project)
            {
                var existingProject = await _sqlContext.Projects.FindAsync(proj.Id);
                if (existingProject != null)
                {
                    _sqlContext.Entry(existingProject).CurrentValues.SetValues(proj);
                }
            }
            await _sqlContext.SaveChangesAsync();
            return true;
        }


        public async Task<List<Project>> GetProjectAsync()
        {
            return await _sqlContext.Projects.Include(x => x.Employees).AsNoTracking().ToListAsync();
        }
    }
}
