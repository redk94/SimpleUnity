using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleUnity.Domain.Services.Interface;
using SImpleUnity.Repository;

namespace SimpleUnity.Testing
{
    /// <summary>
    /// Summary description for SingletonPerResulutionLifetimeTests
    /// </summary>
    [TestClass]
    public class SingletonByPerResulutionLifetimeTests
    {
        [TestMethod]
        public void SingletonByPerResolveLifetimeManger()
        {
            var container = new UnityContainer();

            //Iservice is registerd wiith PerResolveLifetimeManager
            container.RegisterType<IService, AService>(new PerResolveLifetimeManager());

            //parent and child types here have IService as property.
            container.RegisterType<IParent, Parent>();
            container.RegisterType<IChild, Child>();

            //when parent is resolved, Iservice in parent will get AService with perResolveLifetimeManager
            var parent = container.Resolve<IParent>();

            //let's verify serive in both instance has same reference
            Assert.AreSame(parent.Service, parent.Child.Service);
            Assert.IsTrue(parent.Service.Id == parent.Child.Service.Id);
        }
    }

    #region Helper Class
    //IService
    public interface IService
    {
        int Id { get; set; }
    }

    public class AService : IService
    {
        public int Id { get; set; }
    }

    //IChild
    public interface IChild
    {
        IService Service { get; set; }
    }

    public class Child : IChild
    {
        public IService Service { get; set; }

        public Child(IService service)
        {
            Service = service;
            Service.Id = 100;
        }
    }

    //IParent
    public interface IParent
    {
        IChild Child { get; set; }
        IService Service { get; set; }
    }

    public class Parent : IParent
    {
        public IChild Child { get; set; }
        public IService Service { get; set; }

        public Parent(IChild child, IService service)
        {
            Child = child;
            Service = service;
            Service.Id = 300;
        }
    }
    #endregion
}
