namespace P4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpInspectors", "CheckOuttype", c => c.String(maxLength: 10));
            AddColumn("dbo.AbpInspectors", "CheckOutUserId", c => c.Long());
            AlterColumn("dbo.AbpInspectors", "AccountStatus", c => c.String(maxLength: 20));
            CreateIndex("dbo.AbpInspectors", "CheckOutUserId");
            AddForeignKey("dbo.AbpInspectors", "CheckOutUserId", "dbo.AbpUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AbpInspectors", "CheckOutUserId", "dbo.AbpUsers");
            DropIndex("dbo.AbpInspectors", new[] { "CheckOutUserId" });
            AlterColumn("dbo.AbpInspectors", "AccountStatus", c => c.Short(nullable: false));
            DropColumn("dbo.AbpInspectors", "CheckOutUserId");
            DropColumn("dbo.AbpInspectors", "CheckOuttype");
        }
    }
}
