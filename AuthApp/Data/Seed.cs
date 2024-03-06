using AuthApp.Constants;
using AuthApp.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthApp.Data {
    public class Seed {
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder) {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope()) {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                string adminEmail = "admin-email-example-1@mail.com";
                var admin = await userManager.FindByEmailAsync(adminEmail);
                if (admin == null) {
                    var newAdminUser = new AppUser() {
                        UserName = "admin-1",
                        Email = adminEmail,
                    };
                    await userManager.CreateAsync(newAdminUser, "Admin-password-example-1!");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                adminEmail = "admin-email-example-2@mail.com";
                admin = await userManager.FindByEmailAsync(adminEmail);
                if (admin == null) {
                    var newAdminUser = new AppUser() {
                        UserName = "admin-2",
                        Email = adminEmail,
                    };
                    await userManager.CreateAsync(newAdminUser, "Admin-password-example-2!");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string userEmail = "user-email-example-1@mail.com";
                var user = await userManager.FindByEmailAsync(userEmail);
                if (user == null) {
                    var newAdminUser = new AppUser() {
                        UserName = "user-1",
                        Email = userEmail,
                    };
                    await userManager.CreateAsync(newAdminUser, "User-password-example-1!");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                userEmail = "user-email-example-2@mail.com";
                user = await userManager.FindByEmailAsync(userEmail);
                if (user == null) {
                    var newAdminUser = new AppUser() {
                        UserName = "user-2",
                        Email = userEmail,
                    };
                    await userManager.CreateAsync(newAdminUser, "User-password-example-2!");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }
            }
        }
    }
}
