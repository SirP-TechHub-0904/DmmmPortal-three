namespace DmmmPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class latestsch : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SchoolPortalDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteName = c.String(),
                        SchoolName = c.String(),
                        SchoolAddress = c.String(),
                        SchoolCurrentPrincipal = c.String(),
                        ClassCount = c.String(),
                        EnrolStudentsCount = c.String(),
                        UnEnrolStudentsCount = c.String(),
                        TotalStudentsCount = c.String(),
                        PortalUrl = c.String(),
                        WebUrl = c.String(),
                        DateCeated = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                        SchoolType = c.String(),
                        Usedcard = c.String(),
                        NonUsedcard = c.String(),
                        Totalcard = c.String(),
                        TotalStaff = c.String(),
                        CurrentSession = c.String(),
                        Session = c.String(),
                        Term = c.String(),
                        BatchResultPrint = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SchoolPortalDatas");
        }
    }
}
