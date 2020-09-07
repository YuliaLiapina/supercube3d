namespace SuperCube3D_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConfigurePlayerScoreRelation : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Scores", newName: "HighScores");
            DropForeignKey("dbo.HighScores", "Player_Id", "dbo.AspNetUsers");
            DropIndex("dbo.HighScores", new[] { "Player_Id" });
            RenameColumn(table: "dbo.HighScores", name: "Player_Id", newName: "PlayerId");
            AlterColumn("dbo.HighScores", "PlayerId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.HighScores", "PlayerId");
            AddForeignKey("dbo.HighScores", "PlayerId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HighScores", "PlayerId", "dbo.AspNetUsers");
            DropIndex("dbo.HighScores", new[] { "PlayerId" });
            AlterColumn("dbo.HighScores", "PlayerId", c => c.String(maxLength: 128));
            RenameColumn(table: "dbo.HighScores", name: "PlayerId", newName: "Player_Id");
            CreateIndex("dbo.HighScores", "Player_Id");
            AddForeignKey("dbo.HighScores", "Player_Id", "dbo.AspNetUsers", "Id");
            RenameTable(name: "dbo.HighScores", newName: "Scores");
        }
    }
}
