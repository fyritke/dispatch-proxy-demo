using OpenTelemetry.Trace;
using System;
using System.Reflection;

namespace WorkerService1
{
    public class TraceDecorator<TDecorated> : DispatchProxy
    {
        private TDecorated _decorated;
        private string _typeName;

        public static TDecorated Create(TDecorated decorated)
        {
            object proxy = Create<TDecorated, TraceDecorator<TDecorated>>()!;
            ((TraceDecorator<TDecorated>)proxy!).SetParameters(decorated);

            return (TDecorated)proxy;
        }

        private void SetParameters(TDecorated decorated)
        {
            _decorated = decorated;
            _typeName = decorated.GetType().Name;
        }

        protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
        {
            string activityName = $"{_typeName}.{targetMethod?.Name}";
            using var activity = Telemetry.ActivitySource.StartActivity(activityName);

            try
            {
                var result = targetMethod?.Invoke(_decorated, args);

                return result;
            }
            catch (Exception ex)
            {
                activity.RecordException(ex);
                throw ex.InnerException ?? ex;
            }
        }
    }
}
