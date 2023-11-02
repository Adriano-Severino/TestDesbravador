using Microsoft.Extensions.Hosting;
using Modelo.Domain.Entities;
using Modelo.Domain.Enums;
using Modelo.Domain.Interfaces;

namespace Modelo.Service.Services
{
    public class TaskUpdateProject<TEntity> : IHostedService, IDisposable where TEntity : Project
    {
        private Timer _timer;
        private IBaseService<Project> _baseProjectService;
        public TaskUpdateProject(IBaseService<Project> baseService)
        {
            _baseProjectService = baseService;
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Cria um novo Timer que chama o método Get a cada 5 minutos
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            // Chama o método Get
            var project = _baseProjectService.Get<Project>().Result;

            foreach (var item in project)
            {
                item.StatusProjectEnum = item.StatusProjectEnum++;
            }

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Para o Timer quando o serviço é parado
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}

