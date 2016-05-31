using SimpleUnity.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleUnity.Repo.Impl.Swift
{
    public class BucketRepository : IBucketRepository
    {
        public void Add()
        {
            Console.WriteLine("Adding a bucket to Swift");
        }
    }
}
