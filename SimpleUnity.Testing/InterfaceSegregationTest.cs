using System;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleUnity.Testing
{
    /// <summary>
    /// multi depth in consturuction cause no explicit concreat class for the top interface.
    /// </summary>
    [TestClass]
    public class InterfaceSegregationTest
    {
        [TestMethod]
        public void Problem_InterfaceSegregationOnBaseInterfaceTest()
        {
            var container = new UnityContainer();

            //actually UsArmy is implecit implatation of IHuman.
            container.RegisterType<IArmy, UsArmy>();
            container.RegisterType<IMilitaryUnit, MilitaryUnit>();
            container.Resolve<IMilitaryUnit>();

            // Resolution of the dependency failed, type = \"SimpleUnity.Testing.IMilitaryUnit\", // name = \"(none)\". 
            // Exception occurred while: while resolving.
            // Exception is: InvalidOperationException - The current type, SimpleUnity.Testing.IHuman, is an interface 
            // and cannot be constructed. Are you missing a type mapping?\
            // At the time of the exception, the container was: Resolving SimpleUnity.Testing.MilitaryUnit,(none) 
            // (mapped from SimpleUnity.Testing.IMilitaryUnit, (none)). Resolving parameter \"human\" of constructor SimpleUnity.
            // Testing.MilitaryUnit(SimpleUnity.Testing.IHuman human). Resolving SimpleUnity.Testing.IHuman,(none)
        }

        [TestMethod]
        public void Solution_InterfaceSegregationOnBaseInterfaceTest()
        {
            var container = new UnityContainer();

            //actually UsArmy is implecit implatation of IHuman.
            container.RegisterType<IArmy, UsArmy>();
            //add a hint to container to search for an implementation of IArmy, even though consructor is looking ro IHuman, the base of IArmy
            container.RegisterType<IMilitaryUnit, MilitaryUnit>(new InjectionConstructor(typeof(IArmy)));

            container.Resolve<IMilitaryUnit>();
        }
    }

    public interface IHuman { }
    internal interface IArmy : IHuman { }
    internal class UsArmy : IArmy { }

    public interface IMilitaryUnit { }

    public class MilitaryUnit : IMilitaryUnit
    {
        public MilitaryUnit(IHuman human)
        {
            
        }
    }


}
