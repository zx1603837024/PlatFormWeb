namespace P4.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class _202203300906202_Initial1 : DbMigration
    {
        public override void Up()
        {
            AlterTableAnnotations(
                "dbo.AbpParkingMonthlyRent",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlateNumber = c.String(),
                        OpenId = c.String(),
                        ParkId = c.Int(nullable: false),
                        ParkingId = c.String(),
                        BeginTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
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
                        "DynamicFilter_ParkingMonthlyRent_MustHaveCompany",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                    { 
                        "DynamicFilter_ParkingMonthlyRent_MustHavePark",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                    { 
                        "DynamicFilter_ParkingMonthlyRent_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AddColumn("dbo.AbpParkingMonthlyRent", "TenantId", c => c.Int(nullable: false));
            AddColumn("dbo.AbpParkingMonthlyRent", "CompanyId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbpParkingMonthlyRent", "CompanyId");
            DropColumn("dbo.AbpParkingMonthlyRent", "TenantId");
            AlterTableAnnotations(
                "dbo.AbpParkingMonthlyRent",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlateNumber = c.String(),
                        OpenId = c.String(),
                        ParkId = c.Int(nullable: false),
                        ParkingId = c.String(),
                        BeginTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
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
                        "DynamicFilter_ParkingMonthlyRent_MustHaveCompany",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                    { 
                        "DynamicFilter_ParkingMonthlyRent_MustHavePark",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                    { 
                        "DynamicFilter_ParkingMonthlyRent_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
        }
    }
}
