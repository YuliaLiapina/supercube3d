namespace SuperCube3D_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlayerAchievementToManyToMany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Achievements", "Player_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Achievements", new[] { "Player_Id" });
            CreateTable(
                "dbo.PlayerAchievements",
                c => new
                    {
                        Player_Id = c.String(nullable: false, maxLength: 128),
                        Achievement_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Player_Id, t.Achievement_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.Player_Id, cascadeDelete: true)
                .ForeignKey("dbo.Achievements", t => t.Achievement_Id, cascadeDelete: true)
                .Index(t => t.Player_Id)
                .Index(t => t.Achievement_Id);
            
            DropColumn("dbo.Achievements", "Player_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Achievements", "Player_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.PlayerAchievements", "Achievement_Id", "dbo.Achievements");
            DropForeignKey("dbo.PlayerAchievements", "Player_Id", "dbo.AspNetUsers");
            DropIndex("dbo.PlayerAchievements", new[] { "Achievement_Id" });
            DropIndex("dbo.PlayerAchievements", new[] { "Player_Id" });
            DropTable("dbo.PlayerAchievements");
            CreateIndex("dbo.Achievements", "Player_Id");
            AddForeignKey("dbo.Achievements", "Player_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
