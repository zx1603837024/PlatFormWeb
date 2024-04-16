namespace P4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PayLevelAddTen : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpPayLevelSettings", "TenantId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbpPayLevelSettings", "TenantId");
        }
    }
}
