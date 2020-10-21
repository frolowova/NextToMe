using Microsoft.AspNetCore.Identity;
using NextToMe.Common.Models;
using NextToMe.Database;
using NextToMe.Database.Entities;
using System;
using System.Threading.Tasks;

namespace NextToMe.API
{
    public static class DatabaseInit
    {
        public static async Task Initialize(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager, ApplicationDbContext context)
        {
            string user1Email = "user@mail.com";
            string user2Email = "user2@mail.com";
            string password = "Abc1234";
            if (await roleManager.FindByNameAsync(Roles.User) == null)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(Roles.User));
            }
            var user1 = await userManager.FindByNameAsync(user1Email);
            if (user1 == null)
            {
                user1 = new User { Email = user1Email, UserName = user1Email };
                IdentityResult result = await userManager.CreateAsync(user1, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user1, Roles.User);
                }
            }
            var user2 = await userManager.FindByNameAsync(user2Email);
            if (user2 == null)
            {
                user2 = new User { Email = user2Email, UserName = user2Email };
                IdentityResult result = await userManager.CreateAsync(user2, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user2, Roles.User);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
