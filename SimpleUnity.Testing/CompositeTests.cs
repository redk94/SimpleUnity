using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleUnity.Repository;

namespace SimpleUnity.Testing
{
    [TestClass]
    public class CompositeTests
    {
        [TestMethod]
        public void BuildComposite()
        {
            var container = new UnityContainer();
            
            container.RegisterType<IBucketRepository, Repo.Impl.Sql.BucketRepository>("SqlBucketRepository");
            container.RegisterType<IBucketRepository, Repo.Impl.Isilon.BucketRepository>("IsilonBucketRepository");
            container.RegisterType<IBucketRepository, Repo.Impl.Swift.BucketRepository>("SwiftBucketRepository");
            
            container.RegisterType<IBucketRepository, CompositeRepository>();

            var repo = container.Resolve<IBucketRepository>();
            Assert.IsInstanceOfType(repo, typeof(CompositeRepository));
        }
    }

    // The class sample can be used in DomainServie level. Let's think about this case. 
    // BlobService need to select eith one or both of Isilon and swift repository at run time.
    // With this pattern, blobService has IBucketRepository[] with both implementation. 
    // The order in array is the order of resistration, which I have no control but hard coding to get instance.
    public class CompositeRepository : IBucketRepository
    {
        public CompositeRepository(IBucketRepository[] repositories)
        {
            Debug.Assert(repositories != null);
            Debug.Assert(repositories.Any());
        }

        public void Add()
        {
            throw new NotImplementedException();
        }
    }
}
