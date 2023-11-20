using Microsoft.AspNetCore.Identity;
using Talabat.Core.Models.Identity;

namespace Talabat.Core.Services
{
    public interface ITokenService
    {

        Task<string> CreateTokenAsync(AppUser appUser , UserManager<AppUser> user);
    }
}
