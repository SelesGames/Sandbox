using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PlatformUsers.Identity.DataContext;
using PlatformUsers.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformUsers.Identity
{
    public class ManagePlatformUsers
    {
            public static async Task<bool> CreateRole(string name)
            {
                var roleManager = new RoleManager<IdentityRole>(
                    new RoleStore<IdentityRole>(new PlatformUserIdentityDbContext()));

                var idResult = await roleManager.CreateAsync(new IdentityRole(name));

                return idResult.Succeeded;
            }


            public static async Task<IdentityResult> CreateUser(PlatformUser user, string password)
            {

                var userManager = new UserManager<PlatformUser>(
                    new UserStore<PlatformUser>(new PlatformUserIdentityDbContext()));

                //Allows for use of email address as username:
                userManager.UserValidator = new UserValidator<PlatformUser>(userManager) { AllowOnlyAlphanumericUserNames = false };


                var idResult = await userManager.CreateAsync(user, password);

                return idResult;

            }

            public static List<PlatformUser> GetUsers()
            {
                var db = new PlatformUserIdentityDbContext();
                var users = db.Users.ToList();

                return users;
            }

            public static PlatformUser GetUser(string userName)
            {
                var db = new PlatformUserIdentityDbContext();
                var user = db.Users.First(u => u.UserName == userName);

                return user;
            }

            public static async Task<IdentityResult> AddUserToRole(string userId, string roleName)
            {
                var userManager = new UserManager<PlatformUser>(
                    new UserStore<PlatformUser>(new PlatformUserIdentityDbContext()));

                //Allows for use of email address as username:
                userManager.UserValidator = new UserValidator<PlatformUser>(userManager) { AllowOnlyAlphanumericUserNames = false };

                var result = await userManager.AddToRoleAsync(userId, roleName);

                return result;
        }
    }
}
