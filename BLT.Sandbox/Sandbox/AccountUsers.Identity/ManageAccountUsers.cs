using AccountUsers.Identity.DataContext;
using AccountUsers.Identity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountUsers.Identity
{
    public class ManageAccountUsers
    {
            public static async Task<bool> CreateRole(string name)
            {
                var roleManager = new RoleManager<IdentityRole>(
                    new RoleStore<IdentityRole>(new AccountUserIdentityDbContext()));

                var idResult = await roleManager.CreateAsync(new IdentityRole(name));

                return idResult.Succeeded;
            }


            public static async Task<IdentityResult> CreateUser(AccountUser user, string password)
            {

                var userManager = new UserManager<AccountUser>(
                    new UserStore<AccountUser>(new AccountUserIdentityDbContext()));

                //Allows for use of email address as username:
                userManager.UserValidator = new UserValidator<AccountUser>(userManager) { AllowOnlyAlphanumericUserNames = false };


                var idResult = await userManager.CreateAsync(user, password);

                return idResult;

            }

            public static List<AccountUser> GetUsers()
            {
                var db = new AccountUserIdentityDbContext();
                var users = db.Users.ToList();

                return users;
            }

            public static AccountUser GetUser(string userName)
            {
                var db = new AccountUserIdentityDbContext();
                var user = db.Users.First(u => u.UserName == userName);

                return user;
            }

            public static async Task<IdentityResult> AddUserToRole(string userId, string roleName)
            {
                var userManager = new UserManager<AccountUser>(
                    new UserStore<AccountUser>(new AccountUserIdentityDbContext()));

                //Allows for use of email address as username:
                userManager.UserValidator = new UserValidator<AccountUser>(userManager) { AllowOnlyAlphanumericUserNames = false };

                var result = await userManager.AddToRoleAsync(userId, roleName);

                return result;
        }
    }
}
