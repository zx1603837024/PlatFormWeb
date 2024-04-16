namespace P4.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class InitDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AbpDataLogItem",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DataLogId = c.Long(nullable: false),
                        Field = c.String(maxLength: 40),
                        FieldName = c.String(maxLength: 50),
                        OriginalValue = c.String(maxLength: 200),
                        NewValue = c.String(maxLength: 200),
                        DataType = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpDatalogs", t => t.DataLogId, cascadeDelete: true)
                .Index(t => t.DataLogId);
            
            CreateTable(
                "dbo.AbpDatalogs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EntityName = c.String(maxLength: 200),
                        Name = c.String(maxLength: 200),
                        EntityKey = c.String(maxLength: 50),
                        OperateType = c.Int(nullable: false),
                        TenantId = c.Int(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Datalog_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AbpBusinessDetail", "PaymentType", c => c.Short(nullable: false));
            AddColumn("dbo.AbpBusinessDetail", "EscapeUserId", c => c.Long());
            AddColumn("dbo.AbpBusinessDetail", "Picture1", c => c.String(maxLength: 100));
            AddColumn("dbo.AbpBusinessDetail", "Picture2", c => c.String(maxLength: 100));
            AddColumn("dbo.AbpBusinessDetail", "Picture3", c => c.String(maxLength: 100));
            CreateIndex("dbo.AbpBusinessDetail", "EscapeUserId");
            AddForeignKey("dbo.AbpBusinessDetail", "EscapeUserId", "dbo.AbpUsers", "Id");
            DropColumn("dbo.AbpWhiteList", "Rebate");
            DropColumn("dbo.AbpWhiteList", "IsActive");
            DropTable("dbo.AbpBlackList",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BlackList_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AbpBlackList",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VehicleType = c.String(maxLength: 50),
                        RelateNumber = c.String(maxLength: 50),
                        IdNumber = c.String(maxLength: 50),
                        CarOwner = c.String(maxLength: 50),
                        Remarks = c.String(maxLength: 500),
                        IsActive = c.Boolean(nullable: false),
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
                    { "DynamicFilter_BlackList_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AbpWhiteList", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.AbpWhiteList", "Rebate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropForeignKey("dbo.AbpDataLogItem", "DataLogId", "dbo.AbpDatalogs");
            DropForeignKey("dbo.AbpBusinessDetail", "EscapeUserId", "dbo.AbpUsers");
            DropIndex("dbo.AbpDataLogItem", new[] { "DataLogId" });
            DropIndex("dbo.AbpBusinessDetail", new[] { "EscapeUserId" });
            DropColumn("dbo.AbpBusinessDetail", "Picture3");
            DropColumn("dbo.AbpBusinessDetail", "Picture2");
            DropColumn("dbo.AbpBusinessDetail", "Picture1");
            DropColumn("dbo.AbpBusinessDetail", "EscapeUserId");
            DropColumn("dbo.AbpBusinessDetail", "PaymentType");
            DropTable("dbo.AbpDatalogs",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Datalog_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpDataLogItem");
        }
    }
}
