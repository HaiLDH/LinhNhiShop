﻿using LinhNhiShop.Model.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace LinhNhiShop.Data
{
    public class LinhNhiShopDbContext : IdentityDbContext<ApplicationUser>
    {
        public LinhNhiShopDbContext() : base("LinhNhiShopConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Footer> Footers { get; set; }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuGroup> MenuGroups { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Page> Pages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<PostTag> PostTags { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }

        public DbSet<Slide> Slides { get; set; }

        public DbSet<SupportOnline> SupportOnlines { get; set; }

        public DbSet<SystemConfig> SystemConfigs { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<VisitorStatistic> VisitorStatistics { get; set; }

        public DbSet<Error> Errors { get; set; }

        public static LinhNhiShopDbContext Create()
        {
            return new LinhNhiShopDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            //2 primary key
            builder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId });
            //1 primary key
            builder.Entity<IdentityUserLogin>().HasKey(i => i.UserId);
        }
    }
}
