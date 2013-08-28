namespace ScriptManage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Logs", "dates", c => c.DateTime(nullable: false));
            DropColumn("dbo.Logs", "userid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Logs", "userid", c => c.Int(nullable: false));
            DropColumn("dbo.Logs", "dates");
        }
    }
}
