using SimpleUnity.Domain.Services.Interface;
using SimpleUnity.Repository;

namespace SimpleUnity.Domain.Services
{
    public class CompositBucketRepository : IBucketRepository
    {   
        private readonly IBucketRepository _sqlBucketRepository;
        private readonly IBucketRepository _isilonBucketRepository;
        private readonly IBucketRepository _swiftBucketRepository;
        private readonly IBucketRepository _s3BucketRepository;

        public CompositBucketRepository(IBucketRepository[] bucketRepositoryList)
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