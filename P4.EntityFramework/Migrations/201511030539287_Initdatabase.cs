namespace P4.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Initdatabase : DbMigration
    {
        public override void Up()
        {
            AlterTableAnnotations(
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
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_BlackList_MustHaveCompany",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                    { 
                        "DynamicFilter_BlackList_MustHaveRegion",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                    { 
                        "DynamicFilter_BlackList_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AddColumn("dbo.AbpBlackList", "TenantId", c => c.Int(nullable: false));
            AddColumn("dbo.AbpBlackList", "CompanyId", c => c.Int(nullable: false));
            DropColumn("dbo.AbpBlackList", "RegionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AbpBlackList", "RegionId", c => c.Int(nullable: false));
            DropColumn("dbo.AbpBlackList", "CompanyId");
            DropColumn("dbo.AbpBlackList", "TenantId");
            AlterTableAnnotations(
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
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_BlackList_MustHaveCompany",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                    { 
                        "DynamicFilter_BlackList_MustHaveRegion",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                    { 
                        "DynamicFilter_BlackList_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
        }
    }
}
