using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace SportsStore.Models
{
    public class IdentitySeedData
    {
        private const string adminUser = "Admin";
        private const string adminPassword = "Secret123$";
        //Seed daya for the EF Identity
        //public static async void EnsurePopulated(IApplicationBuilder app)
        //{
        //    // UserManager<T> is a service by ASP.NET Core Identity for managing users
        //    UserManager<IdentityUser> userManager = app.ApplicationServices
        //    .GetRequiredService<UserManager<IdentityUser>>();
        //    IdentityUser user = await userManager.FindByIdAsync(adminUser);
        //    if (user == null)
        //    {
        //        user = new IdentityUser("Admin");
        //        await userManager.CreateAsync(user, adminPassword);
        //    }
        //}
        //Use data in Azure SQL db
        public static async Task EnsurePopulated(UserManager<IdentityUser> userManager)
        {
            IdentityUser user = await userManager.FindByIdAsync(adminUser);
            if (user == null)
            {
                user = new IdentityUser("Admin");
                await userManager.CreateAsync(user, adminPassword);
            }
        }
    }
}
