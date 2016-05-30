using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace SimpleUnity.PerformanceTuning
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer();

            container.RegisterType<ITestService, TestService>().
                RegisterType<IResource, ExpensiveResource>();
            
            //container.RegisterType<ITestService, TestServiceMakingDelay>().
            //    RegisterType<IResource, ExpensiveResource>();

            Console.WriteLine($"{DateTime.Now} - Resolve Testservice");
            var testService = container.Resolve<ITestService>();
            
            Console.WriteLine($"{DateTime.Now} - Calling DoSomthingels");
            testService.DoSomethingElse();

            Console.WriteLine($"{DateTime.Now} - Calling DoSomthing");
            testService.DoSomething();

            Console.WriteLine($"{DateTime.Now} - Test complete");

            Console.ReadKey();

        }
    }
}
