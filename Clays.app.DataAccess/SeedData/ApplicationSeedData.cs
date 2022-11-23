using System;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Threading.Tasks;
using Clays.app.DataAccess.Entities;
using System.Linq;
using Clays.app.DataAccess.DataContext;

namespace Clays.app.DataAccess.SeedData
{
    public class Authorization
    {
        public enum Roles
        {
            Admin,
            User
        }

        public const string default_firstName = "admin";
        public const string default_lastName = "admin";
        public const string default_username = "admin";
        public const string default_email = "admin@abc.com";
        public const string default_password = "Password1,.";
        public const Roles default_role = Roles.Admin;
    }

    public class ApplicationSeedData
    { 
        public static async Task SeedEssentialsAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
                            ApplicationDbContext context)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.User.ToString()));
            //Seed Default User
            var defaultUser = new ApplicationUser { FirstName = Authorization.default_firstName, LastName = Authorization.default_lastName,
                UserName = Authorization.default_username, Email = Authorization.default_email, EmailConfirmed = true,
                PhoneNumberConfirmed = true };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                await userManager.CreateAsync(defaultUser, Authorization.default_password);
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                await userManager.AddToRoleAsync(user, Authorization.default_role.ToString());
            }
            var defaultUser1 = new ApplicationUser {FirstName="User",LastName="Manager", UserName = "userManager@abc.com", Email = "userManager@abc.com", EmailConfirmed = true, PhoneNumberConfirmed = true };
            if (userManager.Users.All(u => u.Id != defaultUser1.Id))
            {
                await userManager.CreateAsync(defaultUser, "Password1,");
                var user1 = await userManager.FindByEmailAsync(defaultUser.Email);
                await userManager.AddToRoleAsync(user1, "User");
            }
            await context.SaveChangesAsync();

        }

        public static async Task AddDoor(ApplicationDbContext context)
        {
            if (!context.DoorType.Any())
            {
                await context.DoorType.AddAsync(new DoorType { DoorTypeId = 1, Type = "Entry" });
                await context.DoorType.AddAsync(new DoorType { DoorTypeId = 2, Type = "Storage" });
            }
            if (!context.Door.Any())
            {
                await context.Door.AddAsync(new Door { DoorId = 1, DoorName = "EntryDoor", DoorNumber = "101", DoorType = 1 });
                await context.Door.AddAsync(new Door { DoorId = 2, DoorName = "StorageDoor", DoorNumber = "201", DoorType = 2 });
            }
            await context.SaveChangesAsync();
        }
    }
}

