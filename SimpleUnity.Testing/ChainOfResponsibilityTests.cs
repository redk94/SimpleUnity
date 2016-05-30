using System;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleUnity.Testing
{
    /// <summary>
    /// How to use InjectionConsutructor by specifying different implementation as construct parameter.
    /// Actually result will be put a specific implementation to different class that need to have the first implemenation as a link.
    /// For instance you create Class A instance and class B needs the instance of A while both A and B implement IBase. 
    /// Construct of both have IBase parmeter which will get a new instance of IBase if no specific direction is provided.
    /// </summary>
    [TestClass]
    public class ChainOfResponsibilityTests
    {
        [TestMethod]
        public void BuildExpertChain()
        {
            var container = new UnityContainer();

            //this will show what registration gets injected where
            container.RegisterType<IExpert, LastExpert>("last");

            container.RegisterType<IExpert, SecondExpert>("second",new InjectionConstructor(new ResolvedParameter<IExpert>("last")));

            container.RegisterType<IExpert, FirstExpert>(new InjectionConstructor(new ResolvedParameter<IExpert>("second")));

            var firstExpert = container.Resolve<IExpert>();
            firstExpert.Solve();

            Assert.IsInstanceOfType(firstExpert, typeof(FirstExpert));
        }
    }

    #region Helper Class
    internal interface IExpert
    {
        void Solve();
    }

    internal class FirstExpert : IExpert
    {
        private readonly IExpert _nextLink;

        public FirstExpert(IExpert nextLink)
        {
            _nextLink = nextLink;
        }

        public void Solve()
        {
            Console.WriteLine("Try To Solve problem from first expert");

            if (_nextLink == null)
                throw new ArgumentNullException(nameof(_nextLink));

            _nextLink?.Solve();
        }
    }

    internal class SecondExpert : IExpert
    {
        private readonly IExpert _nextLink;

        public SecondExpert(IExpert nextLink)
        {
            if (nextLink == null) throw new ArgumentNullException(nameof(nextLink));
            _nextLink = nextLink;
        }

        public void Solve()
        {
            Console.WriteLine("Try To Solve problem from second expert");

            if (_nextLink == null)
                throw new ArgumentNullException(nameof(_nextLink));

            _nextLink?.Solve();
        }
    }

    internal class LastExpert : IExpert
    {
        public LastExpert()
        {

        }

        public void Solve()
        {
            Console.WriteLine("Try To Solve problem from last expert");
        }
    } 
    #endregion
}