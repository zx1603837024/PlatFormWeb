namespace P4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFaultFlag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpBerths", "IsFaultFlag", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbpBerths", "IsFaultFlag");
        }
    }
}
