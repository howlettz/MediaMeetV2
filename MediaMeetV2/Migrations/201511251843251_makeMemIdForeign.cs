namespace MediaMeetV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makeMemIdForeign : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Profiles", "MemberId");
            RenameColumn(table: "dbo.Profiles", name: "DemographicsID", newName: "MemberID");
            RenameIndex(table: "dbo.Profiles", name: "IX_DemographicsID", newName: "IX_MemberID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Profiles", name: "IX_MemberID", newName: "IX_DemographicsID");
            RenameColumn(table: "dbo.Profiles", name: "MemberID", newName: "DemographicsID");
            AddColumn("dbo.Profiles", "MemberId", c => c.Int(nullable: false));
        }
    }
}
