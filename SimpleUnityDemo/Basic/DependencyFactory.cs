using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace SimpleUnityDemo
{
    //Unity Facade
    public class DependencyFactory
    {
        private static IUnityContainer _container;

        public static IUnityContainer Container
        {
            get { return _container; }
            set { _container = value; }
        }

        static DependencyFactory()
        {
            var container = new UnityContainer();

            var secion = (UnityConfigurationSection)null;
            try
            {
                secion = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            }
            catch(Exception ex)
            {
                throw;
            }
            

            if(secion != null)
            {
                secion.Configure(container);
            }

            _container = container;
        }

        public static T Resolve<T>()
        {
            T ret = default(T);

            if(Container.IsRegistered(typeof(T)))
            {
                ret = Container.Resolve<T>();
            }

            return ret;
        }
        public static IEnumerable<T> ResolveAll<T>()
        {
         
            return Container.ResolveAll<T>();
        }

    }
}

