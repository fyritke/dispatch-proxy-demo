using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System;

namespace WorkerService1
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureTelemetry(this IServiceCollection services)
        {
            services.AddOpenTelemetryTracing(tracerProviderBuilder =>
            {
                tracerProviderBuilder
                    .AddSource(Telemetry.SERVICE_NAME)
                    .SetResourceBuilder(
                       ResourceBuilder
                           .CreateDefault()
                           .AddService(serviceName: Telemetry.SERVICE_NAME)
                       )
                    .AddAspNetCoreInstrumentation(options =>
                    {
                        options.Enrich = (activity, eventName, rawObject) =>
                        {
                            if (eventName == "OnException")
                            {
                                if (rawObject is Exception exception)
                                {
                                    activity.SetTag("StackTrace", exception.StackTrace);
                                }
                            }
                        };
                    })
                    .AddConsoleExporter();

                //tracerProviderBuilder.AddHoneycomb(options =>
                //{
                //    options.ServiceName = Telemetry.SERVICE_NAME;
                //    options.ApiKey = "<your key here>";
                //});
            });

            return services;
        }
    }

}
