namespace P4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDatabase2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpParks", "BerthCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbpParks", "BerthCount");
        }
    }
}
