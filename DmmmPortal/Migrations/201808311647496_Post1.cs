namespace DmmmPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Post1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Name", c => c.String());
            AddColumn("dbo.Comments", "Email", c => c.String());
            DropColumn("dbo.Comments", "Username");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "Username", c => c.String());
            DropColumn("dbo.Comments", "Email");
            DropColumn("dbo.Comments", "Name");
        }
    }
}
