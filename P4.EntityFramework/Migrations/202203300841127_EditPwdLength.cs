namespace P4.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class EditPwdLength : DbMigration
    {
        public override void Up()
        {
            AlterTableAnnotations(
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
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_ParkChannel_MustHavePark",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
        }
        
        public override void Down()
        {
            AlterTableAnnotations(
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
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_ParkChannel_MustHavePark",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
        }
    }
}
