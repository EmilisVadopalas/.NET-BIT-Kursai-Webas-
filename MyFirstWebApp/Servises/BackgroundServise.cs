using Microsoft.EntityFrameworkCore;
using MyFirstWebApp.Database;
using MyFirstWebApp.Database.Entities;
using MyFirstWebApp.Servises.Contracts;

namespace MyFirstWebApp.Servises
{
    public class BackgroundScrapperServise : IHostedService, IDisposable
    {
        private readonly int _runEveryMinutes;
        private readonly IServiceScopeFactory _scopeFactory;

        private Timer _timer;

        public BackgroundScrapperServise(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            _runEveryMinutes = 15;
        }

        public Task StartAsync(CancellationToken token)
        {
            _timer = new Timer(async (state) => await RefreshProcessors(), null, TimeSpan.Zero, TimeSpan.FromMinutes(_runEveryMinutes));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken token)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        private async Task RefreshProcessors()
        {
            using var scope = _scopeFactory.CreateAsyncScope();
            var dbContext = (WebDatabaseContext)scope.ServiceProvider.GetRequiredService(typeof(WebDatabaseContext));
            var processorServise = (ITopoProccessorsServise)scope.ServiceProvider.GetService(typeof(ITopoProccessorsServise));

            if (processorServise != null)
            {
                var processors = await processorServise.ScrapeTopoProcesors();

                dbContext.Processors.RemoveRange(dbContext.Processors);

                var processorEntities = new List<Processor>();

                foreach (var processor in processors)
                {
                    processorEntities.Add(new Processor
                    {
                        Name = processor.Name,
                        PictureUrl = processor.PictureUrl,
                        Price = processor.Price,
                        ProductLink = processor.ProductLink
                    });
                }

                dbContext.AddRange(processorEntities);

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
