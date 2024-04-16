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
                "dbo.AbpPrintAds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdName = c.String(),
                        Context = c.String(maxLength: 30),
                        QrCode = c.String(maxLength: 30),
                        BeginTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        TenantId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PrintAd_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AbpPrintAds",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PrintAd_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
