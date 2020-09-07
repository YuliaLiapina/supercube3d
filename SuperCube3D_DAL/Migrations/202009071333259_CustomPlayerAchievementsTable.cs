namespace SuperCube3D_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomPlayerAchievementsTable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.PlayerAchievements", name: "Player_Id", newName: "PlayerId");
            RenameColumn(table: "dbo.PlayerAchievements", name: "Achievement_Id", newName: "AchievementId");
            RenameIndex(table: "dbo.PlayerAchievements", name: "IX_Player_Id", newName: "IX_PlayerId");
            RenameIndex(table: "dbo.PlayerAchievements", name: "IX_Achievement_Id", newName: "IX_AchievementId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.PlayerAchievements", name: "IX_AchievementId", newName: "IX_Achievement_Id");
            RenameIndex(table: "dbo.PlayerAchievements", name: "IX_PlayerId", newName: "IX_Player_Id");
            RenameColumn(table: "dbo.PlayerAchievements", name: "AchievementId", newName: "Achievement_Id");
            RenameColumn(table: "dbo.PlayerAchievements", name: "PlayerId", newName: "Player_Id");
        }
    }
}
