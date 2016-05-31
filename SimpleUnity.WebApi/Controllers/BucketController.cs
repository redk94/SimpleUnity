using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SimpleUnity.Domain.Services.Interface;

namespace SimpleUnity.WebApi.Controllers
{
    public class BucketController : ApiController
    {
        private readonly IAdminService _adminService;


        public BucketController(IAdminService adminService)
        {
            _adminService = adminService;
        }


        // GET: api/Bucket
        public IEnumerable<string> Get()
        {
            _adminService.AddBucket();

            return new string[] { "value1", "value2" };
        }

        // GET: api/Bucket/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Bucket
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Bucket/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Bucket/5
        public void Delete(int id)
        {
        }
    }
}
