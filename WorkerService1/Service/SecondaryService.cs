using System.Threading;

namespace WorkerService1
{
    public class SecondaryService : ISimpleService
    {
        public void SimpleMethod()
        {
            Thread.Sleep(1000);
        }

        public void MethodWithNestedMethod()
        {
            Thread.Sleep(1000);
            SimpleMethod();
        }
    }
}
