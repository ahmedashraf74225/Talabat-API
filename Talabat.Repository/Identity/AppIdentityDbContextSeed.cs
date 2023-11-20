using Microsoft.AspNetCore.Identity;
using Talabat.Core.Models.Identity;

namespace Talabat.Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public async static Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var appUser = new AppUser()
                {

                    DisplayName = "Ahmed Ashraf",
                    Email = "ahmedashraf74225@gmail.com",
                    UserName = "Ahmed.Ashraf",
                    PhoneNumber = "01095349803"
                };

                await userManager.CreateAsync(appUser, "Pa$$w0rd");

            }
        }
    }
}
