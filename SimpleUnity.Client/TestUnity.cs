using SimpleUnity.Domain.Services.Interface;

namespace SimpleUnity.Client
{
    public class TestUnity
    {
        private readonly IAdminService _adminService;

        public TestUnity()
        {
        }

        public TestUnity(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public void Run()
        {
            _adminService.AddBucket();
        }
    }
}