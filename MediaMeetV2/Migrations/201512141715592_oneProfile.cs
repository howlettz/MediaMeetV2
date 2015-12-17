namespace MediaMeetV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class oneProfile : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProfileFriends", "Profile_Id", "dbo.Profiles");
            DropForeignKey("dbo.ProfileFriends", "Friend_Id", "dbo.Friends");
            DropIndex("dbo.ProfileFriends", new[] { "Profile_Id" });
            DropIndex("dbo.ProfileFriends", new[] { "Friend_Id" });
            AddColumn("dbo.Friends", "assocProfile_Id", c => c.Int());
            CreateIndex("dbo.Friends", "assocProfile_Id");
            AddForeignKey("dbo.Friends", "assocProfile_Id", "dbo.Profiles", "Id");
            DropTable("dbo.ProfileFriends");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProfileFriends",
                c => new
                    {
                        Profile_Id = c.Int(nullable: false),
                        Friend_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Profile_Id, t.Friend_Id });
            
            DropForeignKey("dbo.Friends", "assocProfile_Id", "dbo.Profiles");
            DropIndex("dbo.Friends", new[] { "assocProfile_Id" });
            DropColumn("dbo.Friends", "assocProfile_Id");
            CreateIndex("dbo.ProfileFriends", "Friend_Id");
            CreateIndex("dbo.ProfileFriends", "Profile_Id");
            AddForeignKey("dbo.ProfileFriends", "Friend_Id", "dbo.Friends", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProfileFriends", "Profile_Id", "dbo.Profiles", "Id", cascadeDelete: true);
        }
    }
}
