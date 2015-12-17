namespace MediaMeetV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InterestsReferBack : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Interests", "Profile_Id", "dbo.Profiles");
            DropIndex("dbo.Interests", new[] { "Profile_Id" });
            CreateTable(
                "dbo.ProfileInterests",
                c => new
                    {
                        Profile_Id = c.Int(nullable: false),
                        Interest_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Profile_Id, t.Interest_Id })
                .ForeignKey("dbo.Profiles", t => t.Profile_Id, cascadeDelete: true)
                .ForeignKey("dbo.Interests", t => t.Interest_Id, cascadeDelete: true)
                .Index(t => t.Profile_Id)
                .Index(t => t.Interest_Id);
            
            DropColumn("dbo.Interests", "Profile_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Interests", "Profile_Id", c => c.Int());
            DropForeignKey("dbo.ProfileInterests", "Interest_Id", "dbo.Interests");
            DropForeignKey("dbo.ProfileInterests", "Profile_Id", "dbo.Profiles");
            DropIndex("dbo.ProfileInterests", new[] { "Interest_Id" });
            DropIndex("dbo.ProfileInterests", new[] { "Profile_Id" });
            DropTable("dbo.ProfileInterests");
            CreateIndex("dbo.Interests", "Profile_Id");
            AddForeignKey("dbo.Interests", "Profile_Id", "dbo.Profiles", "Id");
        }
    }
}
