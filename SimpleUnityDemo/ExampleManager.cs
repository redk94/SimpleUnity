using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleUnityDemo
{
    public class ExampleManager : IExampleManager
    {
        public void DoWork()
        {
            Console.WriteLine("Do Manager's Job");
        }
    }
}
