using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WorkerService1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using var app = CreateHostBuilder(args).Build();
            app.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<ISimpleService, SecondaryService>()
                            .Decorate<ISimpleService>(service => TraceDecorator<ISimpleService>.Create(service));

                    services.AddSingleton<IMainService, MainService>()
                            .Decorate<IMainService>(service => TraceDecorator<IMainService>.Create(service));

                    services.AddHostedService<Worker>();

                    services.ConfigureTelemetry();
                });
    }
}
