namespace Restaurant.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Restaurant.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Restaurant.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Restaurant.Models.ApplicationDbContext context)
        {
            var roleManager =
             new RoleManager<IdentityRole>(
                 new RoleStore<IdentityRole>(context));


            var userManager =
                new UserManager<ApplicationUser>(
                        new UserStore<ApplicationUser>(context));

            if (!context.Roles.Any(p => p.Name == "Admin"))
            {
                var adminRole = new IdentityRole("Admin");
                roleManager.Create(adminRole);
            }

            if (!context.Roles.Any(p => p.Name == "Moderator"))
            {
                var moderatorRole = new IdentityRole("Moderator");
                roleManager.Create(moderatorRole);
            }

            ApplicationUser adminUser;
            ApplicationUser moderatorUser;

            if (!context.Users.Any(p => p.UserName == "admin@restaurant.com"))
            {
                adminUser = new ApplicationUser();
                adminUser.UserName = "admin@restaurant.com";
                adminUser.Email = "admin@restaurant.com";

                userManager.Create(adminUser, "Password-1");
            }
            else
            {
                adminUser = context.Users.First(p => p.UserName == "admin@restaurant.com");
            }

            if (!context.Users.Any(p => p.UserName == "moderator@restaurant.com"))
            {
                moderatorUser = new ApplicationUser();
                moderatorUser.Email = "moderator@restaurant.com";
                moderatorUser.UserName = "moderator@restaurant.com";

                userManager.Create(moderatorUser, "Password-1`");
            }
            else
            {
                moderatorUser = context.Users.First(p => p.UserName == "moderator@restaurant.com");
            }

            if (!userManager.IsInRole(adminUser.Id, "Admin"))
            {
                userManager.AddToRole(adminUser.Id, "Admin");
            }

            if (!userManager.IsInRole(moderatorUser.Id, "Moderator"))
            {
                userManager.AddToRole(moderatorUser.Id, "Moderator");
            }
        }
    }
}
