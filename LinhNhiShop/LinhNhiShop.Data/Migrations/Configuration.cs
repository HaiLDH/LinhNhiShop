namespace LinhNhiShop.Data.Migrations
{
    using LinhNhiShop.Model.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LinhNhiShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LinhNhiShopDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            //  Test login with Available Account in DB
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new LinhNhiShopDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new LinhNhiShopDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "linhmieu",
                Email = "haild.dev8@gmail.com",
                EmailConfirmed = true,
                Birthday = DateTime.Now,
                FullName = "Pham Dieu Linh"
            };

            manager.Create(user, "linhlunglinh");

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = manager.FindByEmail("haild.dev8@gmail.com");
            manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
