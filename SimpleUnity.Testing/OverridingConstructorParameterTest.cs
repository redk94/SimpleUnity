using System;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleUnity.Testing
{
    [TestClass]
    public class OverridingConstructorParameterTest
    {
        [TestMethod]
        public void OverridingCtorParameterTest()
        {
            var container = new UnityContainer();
            container.RegisterType<MyType>();

            container.RegisterType<MyType>(new InjectionConstructor("Holy"));

            var instance1 = container.Resolve<MyType>();
            Assert.AreEqual("Holy", instance1.Message);

            var instance2 = container.Resolve<MyType>( new ParameterOverride("message", "Shit"));
            Assert.AreEqual("shit", instance2.Message);
        }
    }


    public class MyType
    {
        public string Message { get; set; }

        public MyType(string message)
        {
            Message = message;
        }
    }
}
