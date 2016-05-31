using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Threading;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleUnity.Repository;

namespace SimpleUnity.Testing
{
    [TestClass]
    public class FactoryTest
    {
        [TestMethod]
        public void UnityBasedFactoryTest()
        {
            var container = new UnityContainer();

            //add types = it's working with out this, but how???
            //container.RegisterType<IBlobRepository, Repo.Impl.Isilon.BlobRepository>();
            //container.RegisterType<IBlobRepository, Repo.Impl.Swift.BlobRepository>();
            //container.RegisterType<IBucketRepository, Repo.Impl.Isilon.BucketRepository>();

            //add factory
            container.RegisterType<IRepositoryFactory, RepositoryFactory>();

            //get factory
            var factory = container.Resolve<IRepositoryFactory>();
            
            //test
            var isilonRepo = factory.CreateBucketRepository(RepositoryType.Isilon);
            isilonRepo.Add();

            var isilonRepo1 = factory.CreateBucketRepository(RepositoryType.Isilon);
            isilonRepo1.Add();

            var referenceEquals = ReferenceEquals(isilonRepo, isilonRepo1);


            var sqlRepo = factory.CreateBucketRepository(RepositoryType.Sql);
            sqlRepo.Add();

            var swiftRepo = factory.CreateBucketRepository(RepositoryType.Swift);
            sqlRepo.Add();

        }

        [TestMethod]
        public void FactoryLoadTest()
        {
            Stopwatch watch = new Stopwatch();
            

            for (int i = 0; i < 10; i++)
            {
                watch.Restart();
                    

                var container = new UnityContainer();

                //add types = it's working with out this, but how???
                //container.RegisterType<IBlobRepository, Repo.Impl.Isilon.BlobRepository>();
                //container.RegisterType<IBlobRepository, Repo.Impl.Swift.BlobRepository>();
                //container.RegisterType<IBucketRepository, Repo.Impl.Isilon.BucketRepository>();

                //add factory
                container.RegisterType<IRepositoryFactory, RepositoryFactory>();

                //get factory
                var factory = container.Resolve<IRepositoryFactory>();

                var isilonRepo = factory.CreateBucketRepository(RepositoryType.Isilon);

                var isilonRepo1 = factory.CreateBucketRepository(RepositoryType.Isilon);
                
                var sqlRepo = factory.CreateBucketRepository(RepositoryType.Sql);

                var swiftRepo = factory.CreateBucketRepository(RepositoryType.Swift);

                Debug.WriteLine("{0} - {1} done.", watch.ElapsedMilliseconds, i);
            }
        }
    }

    public enum RepositoryType
    {
        Sql,
        Isilon,
        Swift,
        S3
    }

    public interface IRepositoryFactory
    {
        IBucketRepository CreateBucketRepository(RepositoryType repositoryType);
    }

    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IUnityContainer _container;

        public RepositoryFactory(IUnityContainer container)
        {
            _container = container;
        }

        public IBucketRepository CreateBucketRepository(RepositoryType repositoryType)
        {
            switch (repositoryType)
            {
                case RepositoryType.Isilon:
                    return _container.Resolve<Repo.Impl.Isilon.BucketRepository>();
                case RepositoryType.Sql:
                    return _container.Resolve<Repo.Impl.Sql.BucketRepository>();
                case RepositoryType.Swift:
                    return _container.Resolve<Repo.Impl.Swift.BucketRepository>();
                case RepositoryType.S3:
                default:
                    throw new NotSupportedException(nameof(repositoryType));
            }
        }
    }
}
