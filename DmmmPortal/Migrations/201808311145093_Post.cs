namespace DmmmPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Post : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "IsPublished", c => c.Boolean(nullable: false));
            AddColumn("dbo.ImageModels", "PostId", c => c.Int(nullable: false));
            CreateIndex("dbo.ImageModels", "PostId");
            AddForeignKey("dbo.ImageModels", "PostId", "dbo.Posts", "Id", cascadeDelete: true);
            DropColumn("dbo.Posts", "WhoCanSeePost");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "WhoCanSeePost", c => c.Int(nullable: false));
            DropForeignKey("dbo.ImageModels", "PostId", "dbo.Posts");
            DropIndex("dbo.ImageModels", new[] { "PostId" });
            DropColumn("dbo.ImageModels", "PostId");
            DropColumn("dbo.Posts", "IsPublished");
        }
    }
}
