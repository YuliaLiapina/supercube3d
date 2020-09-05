namespace SuperCube3D_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLoginCounter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "SuccessfulLoginCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "SuccessfulLoginCount");
        }
    }
}
