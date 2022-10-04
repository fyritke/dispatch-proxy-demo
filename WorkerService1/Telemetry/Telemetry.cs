using System;
using System.Diagnostics;

namespace WorkerService1
{
    public static class Telemetry
    {
        public const string SERVICE_NAME = "TestApplication";

        private static readonly Lazy<ActivitySource> _activitySource = new Lazy<ActivitySource>(() => new ActivitySource(SERVICE_NAME));
        public static ActivitySource ActivitySource => _activitySource.Value;
    }
}