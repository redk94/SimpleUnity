using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleUnityDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IExampleService svc = new ExampleService();
            svc.DoWork();
        }
    }
}
