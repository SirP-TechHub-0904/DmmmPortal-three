namespace DmmmPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subsections", "SchoolLogo", c => c.Binary());
            AddColumn("dbo.Subsections", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subsections", "Address");
            DropColumn("dbo.Subsections", "SchoolLogo");
        }
    }
}
