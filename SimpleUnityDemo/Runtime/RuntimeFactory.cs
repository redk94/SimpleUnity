﻿using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleUnityDemo
{
    public class RuntimeFactory
    {
        private static IUnityContainer _container;

        public static IUnityContainer Container
        {
            get { return _container; }
            set { _container = value; }
        }

        static RuntimeFactory()
        {
            var container = new UnityContainer().RegisterType<IExampleService>(new InjectionConstructor());

            IExampleService exampleService = container.Resolve<IExampleService>();
        }

        public static T Resolve<T>()
        {
            T ret = default(T);

            if (Container.IsRegistered(typeof(T)))
            {
                ret = Container.Resolve<T>();
            }

            return ret;
        }
    }
}
