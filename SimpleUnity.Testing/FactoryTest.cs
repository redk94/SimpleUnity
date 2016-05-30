using System;
using System.Diagnostics.Contracts;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SImpleUnity.Repository;

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

            var sqlRepo = factory.CreateBucketRepository(RepositoryType.Sql);
            sqlRepo.Add();

            var swiftRepo = factory.CreateBucketRepository(RepositoryType.Swift);
            sqlRepo.Add();

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
