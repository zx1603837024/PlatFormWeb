namespace P4.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class InitDatabase1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AbpOperationLogs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EntityKey = c.String(maxLength: 50),
                        Name = c.String(maxLength: 200),
                        OperateType = c.Int(nullable: false),
                        TenantId = c.Int(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_OperationLog_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AbpOperationLogs",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_OperationLog_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
