using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleUnity.Client
{
    public static class UnityActivator
    {
        static UnityActivator()
        {
            UnityConfig.GetConfiguredContainer();
        }

        public static void Start()
        {
            //resolver activity
        }

        /// <summary>Disposes the Unity container when the application is shut down.</summary>
        public static void Shutdown()
        {
            var container = UnityConfig.GetConfiguredContainer();
            container.Dispose();
        }
    }
}
