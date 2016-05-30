using System;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleUnity.Testing
{
    [TestClass]
    public class InjectionFactory
    {

        [TestMethod]
        public void TestMethod1()
        {
            
        }
    }

    public static class Main
    {
        public static void Run()
        {
            using (var container = new UnityContainer())
            {
                container.RegisterType<ILocalService, ConcreteService>(
                    new InjectionFactory(c => ServiceFactory()));

                Model model = container.Resolve<Model>();

                Console.WriteLine("\nCalling Execute...");
                model.Execute();
                Console.WriteLine("\nCalling Execute...");
                model.Execute();
            }
        }

        static ILocalService ServiceFactory()
        {
            Console.WriteLine("ServiceFactory called.");
            return new ConcreteService();
        }
    }


    public interface ILocalService
    {
        void MakeTheCall();
    }

    public class ConcreteService : ILocalService
    {
        public ConcreteService()
        {
            Console.WriteLine("ConcreteService instantiated.");
        }

        public void MakeTheCall()
        {
            Console.WriteLine(
              "ConcreteService makes the call.");
        }
    }

    public class Model
    {
        public Model(Func<ILocalService> serviceFinder)
        {
            Console.WriteLine("Model instantiated.");

            _serviceFinder = serviceFinder;
        }

        public void Execute()
        {
            _serviceFinder().MakeTheCall();
        }

        private readonly Func<ILocalService> _serviceFinder;
    }

}
