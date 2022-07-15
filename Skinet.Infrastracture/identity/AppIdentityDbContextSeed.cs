using Microsoft.AspNetCore.Identity;
using Skinet.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Infrastracture.identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserData(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                AppUser appUser = new AppUser
                {
                    DisplayName = "Mudassar Ali",
                    Email = "mudassarali@skinet.com",
                    Address = new Address
                    {
                        FirstName = "Mudasasr",
                        LastName = "Ali",
                        City = "Abbottabad",
                        State = "kpk",
                        ZipCode="22010",
                    }
                };
                await userManager.CreateAsync(appUser,"Smak2022@@");
            }
        }
    }
}
