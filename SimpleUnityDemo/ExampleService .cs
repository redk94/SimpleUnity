using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleUnityDemo
{
    public class ExampleService : IExampleService
    {
        private readonly IExampleManager _exampleManager;

        // let's use basic constructor to init DI raw to show that something has to init DI
        public ExampleService() : this(DependencyFactory.Resolve<IExampleManager>())
        {

        }

        //Constructor with Interface
        public ExampleService(IExampleManager exampleManager)
        {
            _exampleManager = exampleManager;
        }

        public void DoWork()
        {
            _exampleManager.DoWork();
        }
    }
}
