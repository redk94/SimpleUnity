using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using SimpleUnity.Domain.Services.Interface;
using SimpleUnity.Repository;

namespace SimpleUnity.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            UnityActivator.Start();

            var continer = UnityConfig.GetConfiguredContainer();
            var repoList1 = continer.ResolveAll<IBucketRepository>();
            var repoList2 = continer.ResolveAll<IBucketRepository>();

            List<IBucketRepository> bucketInstanceList1 = new List<IBucketRepository>();
            List<IBucketRepository> bucketInstanceList2 = new List<IBucketRepository>();


            foreach (var repo in repoList1)
            {
                var type = repo.GetType();
                Console.WriteLine("Type: {0}", type);
                bucketInstanceList1.Add(repo);
            }

            foreach (var repo in repoList2)
            {
                var type = repo.GetType();
                Console.WriteLine("Type: {0}", type);
                bucketInstanceList2.Add(repo);
            }



            for (int i = 0; i < 3; i++)
            {
                try
                {
                    var refEqul = object.ReferenceEquals(bucketInstanceList1[i], bucketInstanceList2[i]);
                    Console.WriteLine("Are equal: {0}", refEqul);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                }
                
            }


        }
    }
}
