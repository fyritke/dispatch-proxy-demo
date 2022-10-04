using System.Threading;

namespace WorkerService1
{
    public class MainService : IMainService
    {
        private readonly ISimpleService secondaryService;

        public MainService(ISimpleService service)
        {        
            this.secondaryService = service;
        }

        public void SimpleMethod()
        {
            Thread.Sleep(1000);
        }

        public void MethodWithNestedMethod()
        {
            Thread.Sleep(1000);
            SimpleMethod();
        }

        public void CallSecondaryServiceNestedMethod()
        {
            this.secondaryService.MethodWithNestedMethod();
        }
    }
}
