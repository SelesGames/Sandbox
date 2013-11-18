using AgencyUsers.Identity.DataContext;
using AgencyUsers.Identity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyUsers.Identity
{
    public class ManageAgencyUsers
    {
            public static async Task<bool> CreateRole(string name)
            {
                var roleManager = new RoleManager<IdentityRole>(
                    new RoleStore<IdentityRole>(new AgencyUserIdentityDbContext()));

                var idResult = await roleManager.CreateAsync(new IdentityRole(name));

                return idResult.Succeeded;
            }


            public static async Task<IdentityResult> CreateUser(AgencyUser user, string password)
            {

                var userManager = new UserManager<AgencyUser>(
                    new UserStore<AgencyUser>(new AgencyUserIdentityDbContext()));

                //Allows for use of email address as username:
                userManager.UserValidator = new UserValidator<AgencyUser>(userManager) { AllowOnlyAlphanumericUserNames = false };


                var idResult = await userManager.CreateAsync(user, password);

                return idResult;

            }

            public static List<AgencyUser> GetUsers()
            {
                var db = new AgencyUserIdentityDbContext();
                var users = db.Users.ToList();

                return users;
            }

            public static AgencyUser GetUser(string userName)
            {
                var db = new AgencyUserIdentityDbContext();
                var user = db.Users.First(u => u.UserName == userName);

                return user;
            }

            public static async Task<IdentityResult> AddUserToRole(string userId, string roleName)
            {
                var userManager = new UserManager<AgencyUser>(
                    new UserStore<AgencyUser>(new AgencyUserIdentityDbContext()));

                //Allows for use of email address as username:
                userManager.UserValidator = new UserValidator<AgencyUser>(userManager) { AllowOnlyAlphanumericUserNames = false };

                var result = await userManager.AddToRoleAsync(userId, roleName);

                return result;
        }
    }
}
