namespace MediaMeetV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FriendChanges : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProfileInterests", newName: "InterestProfiles");
            DropForeignKey("dbo.Friends", "Profile_Id", "dbo.Profiles");
            DropIndex("dbo.Friends", new[] { "Profile_Id" });
            DropPrimaryKey("dbo.InterestProfiles");
            CreateTable(
                "dbo.ProfileFriends",
                c => new
                    {
                        Profile_Id = c.Int(nullable: false),
                        Friend_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Profile_Id, t.Friend_Id })
                .ForeignKey("dbo.Profiles", t => t.Profile_Id, cascadeDelete: true)
                .ForeignKey("dbo.Friends", t => t.Friend_Id, cascadeDelete: true)
                .Index(t => t.Profile_Id)
                .Index(t => t.Friend_Id);
            
            AddPrimaryKey("dbo.InterestProfiles", new[] { "Interest_Id", "Profile_Id" });
            DropColumn("dbo.Friends", "Profile_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Friends", "Profile_Id", c => c.Int());
            DropForeignKey("dbo.ProfileFriends", "Friend_Id", "dbo.Friends");
            DropForeignKey("dbo.ProfileFriends", "Profile_Id", "dbo.Profiles");
            DropIndex("dbo.ProfileFriends", new[] { "Friend_Id" });
            DropIndex("dbo.ProfileFriends", new[] { "Profile_Id" });
            DropPrimaryKey("dbo.InterestProfiles");
            DropTable("dbo.ProfileFriends");
            AddPrimaryKey("dbo.InterestProfiles", new[] { "Profile_Id", "Interest_Id" });
            CreateIndex("dbo.Friends", "Profile_Id");
            AddForeignKey("dbo.Friends", "Profile_Id", "dbo.Profiles", "Id");
            RenameTable(name: "dbo.InterestProfiles", newName: "ProfileInterests");
        }
    }
}
