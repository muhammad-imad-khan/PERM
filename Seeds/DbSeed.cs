using Microsoft.AspNetCore.Identity;
using PERM.Constants;
using PERM.Models;

namespace PERM.Seeds
{
    public class DbSeed
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            //Seed Roles
            var userManager = service.GetService<UserManager<ApplicationUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();
            await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.PowerUser.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.HrAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.ProjectManager_TeamLead.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.BasicUser.ToString()));

            // creating admin

            var user = new ApplicationUser
            {
                UserName = "imad@gmail.com",
                Email = "imad@gmail.com",
                Name = "ImadKhan",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            var userInDb = await userManager.FindByEmailAsync(user.Email);
            if (userInDb == null)
            {
                await userManager.CreateAsync(user, "Imad@123");
                await userManager.AddToRoleAsync(user, Roles.SuperAdmin.ToString());
            }
        }
    }
}
