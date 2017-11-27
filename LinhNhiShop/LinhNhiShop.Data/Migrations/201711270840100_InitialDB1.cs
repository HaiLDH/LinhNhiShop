namespace LinhNhiShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDB1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductCategories", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.Pages", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.PostCategories", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.Posts", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Status");
            DropColumn("dbo.PostCategories", "Status");
            DropColumn("dbo.Pages", "Status");
            DropColumn("dbo.ProductCategories", "Status");
            DropColumn("dbo.Products", "Status");
        }
    }
}
