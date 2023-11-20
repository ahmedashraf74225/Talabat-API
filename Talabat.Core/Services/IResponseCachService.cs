using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Services
{
    public interface IResponseCachService
    {
        Task CachResponseAsync(string cachKey, object response, TimeSpan liveTime);

        Task<string> GetCachedResponseAsync(string cachKey);
    }
}
