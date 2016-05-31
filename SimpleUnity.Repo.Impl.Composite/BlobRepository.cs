using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleUnity.Repository;

namespace SimpleUnity.Repo.Impl.Composite
{
    public class BucketRepository : IBucketRepository
    {
        private readonly IBucketRepository _sqlBucketRepository;
        private readonly IBucketRepository _isilonBucketRepository;
        private readonly IBucketRepository _swiftBucketRepository;
        private readonly IBucketRepository _s3BucketRepository;

        public BucketRepository(IBucketRepository[] bucketRepositoryList)
        {
            _sqlBucketRepository = bucketRepositoryList[0];
            _isilonBucketRepository = bucketRepositoryList[1];
            _swiftBucketRepository = bucketRepositoryList[2];
        }

        public void Add()
        {
            _sqlBucketRepository.Add();
            _isilonBucketRepository.Add();
        }
    }
}
