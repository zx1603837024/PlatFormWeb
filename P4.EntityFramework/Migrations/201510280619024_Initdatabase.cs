namespace P4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initdatabase : DbMigration
    {
        public override void Up()
        {
          
            CreateIndex("dbo.AbpBerths", "ParkId");
            CreateIndex("dbo.AbpBerths", "TenantId");
           
            AddForeignKey("dbo.AbpBerths", "ParkId", "dbo.AbpParks", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AbpBerths", "TenantId", "dbo.AbpTenants", "Id", cascadeDelete: true);
           
        }
        
        public override void Down()
        {
           
            DropForeignKey("dbo.AbpBerths", "TenantId", "dbo.AbpTenants");
            DropForeignKey("dbo.AbpBerths", "ParkId", "dbo.AbpParks");
           
            DropIndex("dbo.AbpBerths", new[] { "TenantId" });
            DropIndex("dbo.AbpBerths", new[] { "ParkId" });
           
        }
    }
}
