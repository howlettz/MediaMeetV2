namespace MediaMeetV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Demographics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        city = c.String(),
                        state = c.String(),
                        country = c.String(),
                        birthDate = c.DateTime(nullable: false),
                        gender = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberID = c.Int(nullable: false),
                        dateFriended = c.DateTime(nullable: false),
                        Profile_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profiles", t => t.Profile_Id)
                .Index(t => t.Profile_Id);
            
            CreateTable(
                "dbo.Interests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        description = c.String(),
                        Profile_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profiles", t => t.Profile_Id)
                .Index(t => t.Profile_Id);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        userName = c.String(nullable: false),
                        memberName = c.String(nullable: false),
                        dateJoined = c.DateTime(nullable: false),
                        lastLogin = c.DateTime(nullable: false),
                        ProfileID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        introduction = c.String(),
                        DemographicsID = c.Int(nullable: false),
                        MemberId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Demographics", t => t.DemographicsID, cascadeDelete: true)
                .ForeignKey("dbo.Members", t => t.MemberId)
                .Index(t => t.DemographicsID)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        recipiantID = c.Int(nullable: false),
                        messageText = c.String(nullable: false),
                        dateSent = c.DateTime(nullable: false),
                        read = c.Boolean(nullable: false),
                        threadID = c.Int(nullable: false),
                        Profile_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profiles", t => t.Profile_Id)
                .Index(t => t.Profile_Id);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        filePath = c.String(nullable: false),
                        description = c.String(nullable: false),
                        dateAdded = c.DateTime(nullable: false),
                        profilePicture = c.Boolean(nullable: false),
                        Profile_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profiles", t => t.Profile_Id)
                .Index(t => t.Profile_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Profiles", "MemberId", "dbo.Members");
            DropForeignKey("dbo.Photos", "Profile_Id", "dbo.Profiles");
            DropForeignKey("dbo.Messages", "Profile_Id", "dbo.Profiles");
            DropForeignKey("dbo.Interests", "Profile_Id", "dbo.Profiles");
            DropForeignKey("dbo.Friends", "Profile_Id", "dbo.Profiles");
            DropForeignKey("dbo.Profiles", "DemographicsID", "dbo.Demographics");
            DropIndex("dbo.Photos", new[] { "Profile_Id" });
            DropIndex("dbo.Messages", new[] { "Profile_Id" });
            DropIndex("dbo.Profiles", new[] { "MemberId" });
            DropIndex("dbo.Profiles", new[] { "DemographicsID" });
            DropIndex("dbo.Interests", new[] { "Profile_Id" });
            DropIndex("dbo.Friends", new[] { "Profile_Id" });
            DropTable("dbo.Photos");
            DropTable("dbo.Messages");
            DropTable("dbo.Profiles");
            DropTable("dbo.Members");
            DropTable("dbo.Interests");
            DropTable("dbo.Friends");
            DropTable("dbo.Demographics");
        }
    }
}
