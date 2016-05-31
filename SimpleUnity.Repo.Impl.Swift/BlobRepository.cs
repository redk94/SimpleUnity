using SimpleUnity.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleUnity.Repo.Impl.Swift
{
    public class BlobRepository : IBlobRepository
    {
        public void Add()
        {
            Console.WriteLine("Adding a blob to Swift");
        }
    }
}
