namespace DmmmPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class latestschd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SchoolPortalDatas", "TotalResultsCount", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SchoolPortalDatas", "TotalResultsCount");
        }
    }
}
