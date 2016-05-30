using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using SimpleUnity.PerformanceTuning;

namespace SimpleUnity.PerformanceTuning
{
    /// <summary>
    /// adding Func, provide lazy construction. 
    /// </summary>
    public class TestService : ITestService
    {
        private IResource _resource;
        private readonly Func<IResource> _resourceFactory;

        public TestService(Func<IResource> resourceFactory)
        {
            _resourceFactory = resourceFactory;
            Console.WriteLine($"{DateTime.Now} - From Ctor in Testservice");
        }

        public void DoSomething()
        {
            Resource.DoSomething();
        }

        public void DoSomethingElse()
        {
            Console.WriteLine("Do somthing that does not require resource");
        }

        private IResource Resource => _resource ?? (_resource = _resourceFactory());
    }
}


// _resource has to initialize first
public class TestServiceMakingDelay : ITestService
{
    private readonly IResource _resource;

    public TestServiceMakingDelay(IResource resource)
    {
        _resource = resource;
    }

    public void DoSomething()
    {
        _resource.DoSomething();
    }

    public void DoSomethingElse()
    {
        Console.WriteLine("Do somthing that does not require resource");
    }
}
