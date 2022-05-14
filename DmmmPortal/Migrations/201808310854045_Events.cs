namespace DmmmPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Events : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Events", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "UserId", c => c.String());
        }
    }
}
