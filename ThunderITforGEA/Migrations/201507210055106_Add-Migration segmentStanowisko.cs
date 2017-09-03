namespace ThunderITforGEA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMigrationsegmentStanowisko : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "segment", c => c.String());
            AddColumn("dbo.AspNetUsers", "stanowisko", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "stanowisko");
            DropColumn("dbo.AspNetUsers", "segment");
        }
    }
}
