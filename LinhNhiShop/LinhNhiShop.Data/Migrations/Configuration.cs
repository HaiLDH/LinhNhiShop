namespace LinhNhiShop.Data.Migrations
{
    using LinhNhiShop.Model.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LinhNhiShop.Data.LinhNhiShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LinhNhiShopDbContext context)
        {
            ProductCategoryTest(context);
            CrearePage(context);

            //  This method will be called after migrating to the latest version.
            //  Test login with Available Account in DB
            //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new LinhNhiShopDbContext()));

            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new LinhNhiShopDbContext()));

            //var user = new ApplicationUser()
            //{
            //    UserName = "linhmieu",
            //    Email = "haild.dev8@gmail.com",
            //    EmailConfirmed = true,
            //    Birthday = DateTime.Now,
            //    FullName = "Pham Dieu Linh"
            //};

            //manager.Create(user, "linhlunglinh");

            //if (!roleManager.Roles.Any())
            //{
            //    roleManager.Create(new IdentityRole { Name = "Admin" });
            //    roleManager.Create(new IdentityRole { Name = "User" });
            //}

            //var adminUser = manager.FindByEmail("haild.dev8@gmail.com");
            //manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
            ////  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            ////  to avoid creating duplicate seed data.
        }

        private void ProductCategoryTest(LinhNhiShopDbContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> listProductCategory = new List<ProductCategory>()
            {
                new ProductCategory(){Name="Nhẫn",Alias="nhan",Status=true},
                new ProductCategory(){Name="Quạt",Alias="quat",Status=true},
                new ProductCategory(){Name="Vòng",Alias="vong",Status=true},
                new ProductCategory(){Name="Album",Alias="album",Status=true}
            };
                context.ProductCategories.AddRange(listProductCategory);
                context.SaveChanges();
            }
        }

        private void CrearePage(LinhNhiShopDbContext context)
        {
            if (context.Pages.Count() == 0)
            {
                var page = new Page()
                {
                    Name = "Giới Thiệu",
                    Alias = "gioi-thieu",
                    Content = @"Where can I get some?
                                There are many variations of passages of Lorem Ipsum available,
                                but the majority have suffered alteration in some form,
                                by injected humour,
                                or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text.All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary,
                                making this the first true generator on the Internet.It uses a dictionary of over 200 Latin words,
                                combined with a handful of model sentence structures,
                                to generate Lorem Ipsum which looks reasonable.The generated Lorem Ipsum is therefore always free from repetition,
                                injected humour,
                                or non - characteristic words etc.",
                    Status = true
                };
                context.Pages.Add(page);
                context.SaveChanges();
            }
        }
    }
}
