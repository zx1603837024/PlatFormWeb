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
 
            AlterTableAnnotations(
                "dbo.AbpBusinessDetail",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        BerthNumber = c.String(maxLength: 20),
                        PlateNumber = c.String(maxLength: 10),
                        CarType = c.Short(nullable: false),
                        Prepaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Receivable = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FactReceive = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Arrearage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CarInTime = c.DateTime(nullable: false),
                        CarOutTime = c.DateTime(),
                        CarPayTime = c.DateTime(),
                        InOperaId = c.Long(nullable: false),
                        InDeviceCode = c.String(maxLength: 16),
                        OutOperaId = c.Long(),
                        OutDeviceCode = c.String(maxLength: 16),
                        ChargeOperaId = c.Long(),
                        ChargeDeviceCode = c.String(maxLength: 16),
                        guid = c.Guid(nullable: false),
                        SensorsInCarTime = c.DateTime(),
                        SensorsOutCarTime = c.DateTime(),
                        SensorsStopTime = c.Int(),
                        SensorsReceivable = c.Decimal(precision: 18, scale: 2),
                        Repayment = c.Decimal(precision: 18, scale: 2),
                        CarRepaymentTime = c.DateTime(),
                        PaymentType = c.Short(nullable: false),
                        EscapeTime = c.DateTime(),
                        EscapePayStatus = c.Short(nullable: false),
                        IsEscapePay = c.Boolean(nullable: false),
                        EscapeOperaId = c.Long(),
                        EscapeUserId = c.Long(),
                        EscapeDeviceCode = c.String(maxLength: 16),
                        EscapeTenantId = c.Int(),
                        PayStatus = c.Short(nullable: false),
                        IsPay = c.Boolean(nullable: false),
                        StopType = c.Short(nullable: false),
                        FeeType = c.Short(nullable: false),
                        StopTime = c.Int(),
                        Picture1 = c.String(maxLength: 100),
                        Picture2 = c.String(maxLength: 100),
                        Picture3 = c.String(maxLength: 100),
                        TenantId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        RegionId = c.Int(nullable: false),
                        ParkId = c.Int(nullable: false),
                        BerthsecId = c.Int(nullable: false),
                        Status = c.Short(nullable: false),
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
                        "DynamicFilter_BusinessDetail_MustHaveCompany",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
          
            
        }
        
        public override void Down()
        {
          
            
            AlterTableAnnotations(
                "dbo.AbpBusinessDetail",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        BerthNumber = c.String(maxLength: 20),
                        PlateNumber = c.String(maxLength: 10),
                        CarType = c.Short(nullable: false),
                        Prepaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Receivable = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FactReceive = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Arrearage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CarInTime = c.DateTime(nullable: false),
                        CarOutTime = c.DateTime(),
                        CarPayTime = c.DateTime(),
                        InOperaId = c.Long(nullable: false),
                        InDeviceCode = c.String(maxLength: 16),
                        OutOperaId = c.Long(),
                        OutDeviceCode = c.String(maxLength: 16),
                        ChargeOperaId = c.Long(),
                        ChargeDeviceCode = c.String(maxLength: 16),
                        guid = c.Guid(nullable: false),
                        SensorsInCarTime = c.DateTime(),
                        SensorsOutCarTime = c.DateTime(),
                        SensorsStopTime = c.Int(),
                        SensorsReceivable = c.Decimal(precision: 18, scale: 2),
                        Repayment = c.Decimal(precision: 18, scale: 2),
                        CarRepaymentTime = c.DateTime(),
                        PaymentType = c.Short(nullable: false),
                        EscapeTime = c.DateTime(),
                        EscapePayStatus = c.Short(nullable: false),
                        IsEscapePay = c.Boolean(nullable: false),
                        EscapeOperaId = c.Long(),
                        EscapeUserId = c.Long(),
                        EscapeDeviceCode = c.String(maxLength: 16),
                        EscapeTenantId = c.Int(),
                        PayStatus = c.Short(nullable: false),
                        IsPay = c.Boolean(nullable: false),
                        StopType = c.Short(nullable: false),
                        FeeType = c.Short(nullable: false),
                        StopTime = c.Int(),
                        Picture1 = c.String(maxLength: 100),
                        Picture2 = c.String(maxLength: 100),
                        Picture3 = c.String(maxLength: 100),
                        TenantId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        RegionId = c.Int(nullable: false),
                        ParkId = c.Int(nullable: false),
                        BerthsecId = c.Int(nullable: false),
                        Status = c.Short(nullable: false),
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
                        "DynamicFilter_BusinessDetail_MustHaveCompany",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            
        }
    }
}
