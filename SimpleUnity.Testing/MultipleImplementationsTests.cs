using System;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleUnity.Repository;

namespace SimpleUnity.Testing
{
    //meet the initial goal to register and load same type with different implemntation, but still not good enough for production level factory...
    [TestClass]
    public class MultipleImplementationsTests
    {
        [TestMethod]
        public void MultipleImplementations_ResolveCorrectly()
        {
            //regiset type with name
            var container = new UnityContainer();
            container.RegisterType<IBucketRepository, Repo.Impl.Sql.BucketRepository>("SqlBucketRepository");
            container.RegisterType<IBucketRepository, Repo.Impl.Swift.BucketRepository>("SwiftBucketRepository");
            container.RegisterType<IBucketRepository, Repo.Impl.Isilon.BucketRepository>("IsilonBucketRepository");

            //resolve type with name
            Assert.IsNotNull(container.Resolve<IBucketRepository>("SqlBucketRepository"));
            var sqlBucketRepo = container.Resolve<IBucketRepository>("SqlBucketRepository");
            sqlBucketRepo.Add();

            Assert.IsNotNull(container.Resolve<IBucketRepository>("SwiftBucketRepository"));
            var swiftBucketRepo = container.Resolve<IBucketRepository>("SwiftBucketRepository");
            swiftBucketRepo.Add();

            Assert.IsNotNull(container.Resolve<IBucketRepository>("IsilonBucketRepository"));
            var isilonBucketRepo = container.Resolve<IBucketRepository>("IsilonBucketRepository");
            isilonBucketRepo.Add();

            //resolve type with name, which will fail
            container.Resolve<IBucketRepository>();
        }
    }
}
