namespace MediaMeetV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTheTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProfileFriends",
                c => new
                {
                    Profile_Id = c.Int(nullable: false),
                    Friend_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.Profile_Id, t.Friend_Id })
                .ForeignKey("dbo.Profile", t => t.Profile_Id, cascadeDelete: true)
                .ForeignKey("dbo.Friends", t => t.Friend_Id, cascadeDelete: true)
                .Index(t => t.Profile_Id)
                .Index(t => t.Friend_Id);
        }
        
        public override void Down()
        {
        }
    }
}
