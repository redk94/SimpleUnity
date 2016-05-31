using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using SimpleUnity.Domain.Services.Interface;
using SimpleUnity.Repository;

namespace SimpleUnity.Testing
{
    [TestClass]
    public class TrivialTest
    {
        [TestMethod]
        public void SimpleResisterAndResolve()
        {
            var container = new UnityContainer();

            container.RegisterType<IBlobRepository, Repo.Impl.Isilon.BlobRepository>();
            var instance1 = container.Resolve<IBlobRepository>();
            Assert.IsNotNull(instance1);

            container.RegisterType<IBucketRepository, Repo.Impl.Isilon.BucketRepository>();
            var instance2 = container.Resolve<IBucketRepository>();
            Assert.IsNotNull(instance2);

            container.RegisterType<IStorageAdminRepository, Repo.Impl.Sql.StorageAdminRepository>();
            var instance3 = container.Resolve<IStorageAdminRepository>();
            Assert.IsNotNull(instance3);

            //if this runs first, IAdminService cannot be resolved due to child dependency.
            container.RegisterType<IAdminService, SimpleUnity.Domain.Services.AdminServices > ();
            var instance4 = container.Resolve<IAdminService>();
            Assert.IsNotNull(instance4);
        }
    }
}
