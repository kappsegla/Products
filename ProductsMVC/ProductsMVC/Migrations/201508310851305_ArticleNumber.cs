namespace ProductsMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArticleNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ArticleNumber", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ArticleNumber");
        }
    }
}
