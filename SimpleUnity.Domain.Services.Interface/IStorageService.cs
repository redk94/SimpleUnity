using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleUnity.Domain.Services.Interface
{
    public interface IStorageService
    {
        void AddBlob();

        void AddBucket();
    }
}
