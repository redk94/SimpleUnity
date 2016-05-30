using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleUnity.Testing
{
    /// <summary>
    /// How to use InjectionProperty by specifying different implementation as property setup
    /// </summary>
    [TestClass]
    public class CircularDependencyTest
    {
        [TestMethod]
        public void ObjectGraphWithCircularDependency()
        {
            var container = new UnityContainer();

            // InjectionProperty("Egg") indicates that when chicken is resolved, Egg-the property of chicken will be resolved together. 
            // To compare result, enalbe/disable 2 resister commane

            //singleton and init property
            container.RegisterType<IChicken, Chicken>(new PerResolveLifetimeManager(), new InjectionProperty("Egg"));
            //no singleton but init property
            //container.RegisterType<IChicken, Chicken>(new InjectionProperty("Egg"));
            //singleton but no init property
            //container.RegisterType<IChicken, Chicken>(new PerResolveLifetimeManager());

            container.RegisterType<IEgg, Egg>();

            var chicken = container.Resolve<IChicken>();

            Assert.AreSame(chicken, chicken.Egg.Chicken);
            Assert.IsTrue(chicken.Id == chicken.Egg.Chicken.Id);
        }
    }

    #region Helper Clasee
    public interface IEgg
    {
        IChicken Chicken { get; set; }
    }

    public interface IChicken
    {
        IEgg Egg { get; set; }
        int Id { get; set; }
    }

    public class Chicken : IChicken
    {
        public Chicken()
        {
            Id = 1234;
        }
        public IEgg Egg { get; set; }
        public int Id { get; set; }
    }

    public class Egg : IEgg
    {
        public IChicken Chicken { get; set; }

        public Egg(IChicken chicken)
        {
            Chicken = chicken;
        }
    } 
    #endregion
}