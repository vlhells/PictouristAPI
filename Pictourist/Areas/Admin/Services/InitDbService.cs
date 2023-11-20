using Microsoft.AspNetCore.Identity;
using PictouristAPI.Areas.Admin.Models;

namespace PictouristAPI.Areas.Admin.Services
{
    public class InitDbService
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "Admin@Admin";
            string adminPwd = "Abc1234%";

            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }

            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email=adminEmail, UserName="Admin" };
                admin.SetBirthdate("2000-02-02");
                IdentityResult result = await userManager.CreateAsync(admin, adminPwd);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
