using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Models.Identity;

namespace Talabat.APIs.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<AppUser?> FindUserWithAddressAsync(this UserManager<AppUser> userManager,ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            //         (                 Linq Query            )  ( Immediate Execution -> element operator )
            var user =  userManager.Users.Include(U => U.Address).FirstOrDefault(U => U.Email == email);

            return user;

        }
    }
}
