using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleUnity.Domain.Services.Interface;
using SimpleUnity.Repo.Impl.Isilon;
using SimpleUnity.Repository;
using SimpleUnity.Domain.Services;

namespace SimpleUnity.Testing
{
    [TestClass]
    public class ObjectBuildupTest
    {
        [TestMethod]
        public void ObjectBuildup()
        {
            var container = new UnityContainer();
            container.RegisterType<IBlobRepository, BlobRepository>();

            // resolving not registered type - ObjectWithDependencies - in current container.
            var instance = container.Resolve<ObjectWithDependencies>();

            Assert.IsNotNull(instance);
        }
        
        [TestMethod]
        public void ObjectBuildupWithChildDependencyTest()
        {
            var container = new UnityContainer();
            container.RegisterType<IAdminService, AdminServices>();

            // resolving not registered type with child - ObjectWithCHildDependencies - in current container.
            var instance = container.Resolve<ObjectWithChildDependencies>();

            //Unity cannot resolved this. In this case, error message will be 'no parameterless coustruct exists'. 
            Assert.IsNull(instance);
        }
    }

    #region Helper class

    public class ObjectWithDependencies
    {
        public ObjectWithDependencies(IBlobRepository blobRepository)
        {
            if (blobRepository == null)
                throw new ArgumentException(nameof(blobRepository));
        }
    }

    public class ObjectWithChildDependencies
    {
        public ObjectWithChildDependencies(IAdminService blobRepository)
        {
            if (blobRepository == null)
                throw new ArgumentException(nameof(blobRepository));
        }
    } 

    #endregion
}
