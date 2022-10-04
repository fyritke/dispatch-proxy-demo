using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerService1
{
    public class Worker : BackgroundService
    {
        private readonly IMainService mainService;
        private readonly IHostApplicationLifetime lifetime;

        public Worker(IMainService service, IHostApplicationLifetime lifetime)
        {
            this.mainService = service;
            this.lifetime = lifetime;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // wait for the console to load before we report any activities
            await Task.Delay(3000);

            using var activity = Telemetry.ActivitySource.StartActivity("Worker Actions");

            this.mainService.MethodWithNestedMethod();
            this.mainService.CallSecondaryServiceNestedMethod();

            this.lifetime.StopApplication();
        }
    }
}
