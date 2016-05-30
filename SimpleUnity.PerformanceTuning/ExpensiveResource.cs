using System;
using System.Threading;

namespace SimpleUnity.PerformanceTuning
{
    public class ExpensiveResource : IResource
    {
        public ExpensiveResource()
        {
            //simulate expensive initialisation
            Console.WriteLine($"{DateTime.Now} - Doing construction will take 3 sec");
            Thread.Sleep(3000);
        }

        public void DoSomething()
        {
            Console.WriteLine("Do something that need expensive resource");
        }
    }
}