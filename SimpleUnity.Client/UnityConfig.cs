using Microsoft.Practices.Unity;
using System;
using Microsoft.Practices.Unity.Configuration;

namespace SimpleUnity.Client
{
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            try
            {
                RegisterTypes(container);
            }
            catch (Exception ex)
            {
                container.Dispose();
                container = null;
                throw;
            }

            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            try
            {
                return container.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        #endregion
                
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();

        }
    }
}
