namespace P4.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AbpParkingRecord", "CarInOperaId", "dbo.AbpEmployees");
            DropIndex("dbo.AbpParkingRecord", new[] { "CarInOperaId" });
            CreateTable(
                "dbo.AbpParkChannel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParkId = c.Int(nullable: false),
                        ZhiBoChannelId = c.String(maxLength: 50),
                        ChannelCode = c.String(maxLength: 50),
                        ChannelName = c.String(maxLength: 50),
                        ChannelDirection = c.String(maxLength: 10),
                        TenantId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ParkChannel_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ParkChannel_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ParkChannel_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("dbo.AbpUsers", t => t.LastModifierUserId)
                .ForeignKey("dbo.AbpParks", t => t.ParkId, cascadeDelete: true)
                .Index(t => t.ParkId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AbpParkChannel", "ParkId", "dbo.AbpParks");
            DropForeignKey("dbo.AbpParkChannel", "LastModifierUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpParkChannel", "DeleterUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpParkChannel", "CreatorUserId", "dbo.AbpUsers");
            DropIndex("dbo.AbpParkChannel", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpParkChannel", new[] { "LastModifierUserId" });
            DropIndex("dbo.AbpParkChannel", new[] { "DeleterUserId" });
            DropIndex("dbo.AbpParkChannel", new[] { "ParkId" });
           
            DropTable("dbo.AbpParkChannel",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ParkChannel_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ParkChannel_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ParkChannel_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
