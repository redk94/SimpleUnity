using System;
using SimpleUnity.Domain.Services.Interface;
using SImpleUnity.Repository;

namespace SimpleUnity.Domain.Services
{
    public class AdminServices : IAdminService
    {
        private readonly IBlobRepository BlobRepository;
        private readonly IBucketRepository BucketRepository;
        private readonly IStorageAdminRepository StorageAdminRepository;

        public AdminServices(IBlobRepository blobRepository, IBucketRepository bucketRepository, IStorageAdminRepository storageAdminRepository)
        {
            this.BlobRepository = blobRepository;
            this.BucketRepository = bucketRepository;
            this.StorageAdminRepository = storageAdminRepository;
        }

        public void AddAccount()
        {
            StorageAdminRepository.AddAccount();
        }

        public void AddBlob()
        {
            BlobRepository.Add();
        }

        public void AddBucket()
        {
            BucketRepository.Add();
        }
    }
}
