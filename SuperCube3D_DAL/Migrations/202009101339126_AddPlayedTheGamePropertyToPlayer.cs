namespace SuperCube3D_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlayedTheGamePropertyToPlayer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "PlayedTheGame", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "PlayedTheGame");
        }
    }
}
