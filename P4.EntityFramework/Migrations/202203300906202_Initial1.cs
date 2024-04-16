namespace P4.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
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
                    { "DynamicFilter_ParkingMonthlyRent_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
           
        }
        
        public override void Down()
        {
            DropTable("dbo.AbpParkingMonthlyRent",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ParkingMonthlyRent_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
