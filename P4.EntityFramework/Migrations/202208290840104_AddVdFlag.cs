namespace P4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVdFlag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpBerths", "IsSourceVideo", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbpBerths", "IsSourceVideo");
        }
    }
}
