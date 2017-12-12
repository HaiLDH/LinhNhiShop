namespace LinhNhiShop.Data.Migrations
{
    using LinhNhiShop.Model.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
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
            ProductCategoryTest(context);
            CreateSlides(context);

            //  This method will be called after migrating to the latest version.

        }

        private void CreateUser(LinhNhiShopDbContext context)
        {
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

        private void CreateSlides(LinhNhiShopDbContext context)
        {
            if (context.Slides.Count() == 0)
            {
                List<Slide> listSlide = new List<Slide>()
                {
                    new Slide(){
                        Name ="Slide1",
                        DisplayOrder =1,
                        Status =true,
                        URL ="#",
                        Image ="/Assets/client/images/bag.jpg",
                        Content=@"<h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>
                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </p>
                                < span class=""on-get"">GET NOW</span>"
                    },
                    new Slide(){
                        Name ="Slide2",
                        DisplayOrder =2,
                        Status =true,
                        URL ="#",
                        Image ="/Assets/client/images/bag1.jpg",
                        Content=@"<h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>
                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </p>
                                <span class=""on-get"">GET NOW</span>"
                    }
                };
                context.Slides.AddRange(listSlide);
                context.SaveChanges();
            }
        }
    }
}
