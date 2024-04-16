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
            DropTable("dbo.AbpVehiclesBlackList",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VehiclesBlackList_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_VehiclesBlackList_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpVehiclesCalculator",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VehiclesCalculator_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_VehiclesCalculator_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpVehiclesError",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VehiclesError_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_VehiclesError_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpVehiclesExit",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VehiclesExit_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_VehiclesExit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpVehiclesFControlBarrierGate",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VehiclesFControlBarrierGate_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_VehiclesFControlBarrierGate_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpVehiclesHandMoveCutOff",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VehiclesHandMoveCutOff_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_VehiclesHandMoveCutOff_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpVehiclesMonthCar",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VehiclesMonthCar_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_VehiclesMonthCar_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpVehiclesPayType",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VehiclesPayType_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_VehiclesPayType_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpVehiclesRecord",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VehiclesRecord_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_VehiclesRecord_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpVehiclesType",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VehiclesType_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_VehiclesType_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AbpVehiclesType",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SCardTypeID = c.Int(nullable: false),
                        SCardTypeName = c.String(maxLength: 50),
                        TenantId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        LastModifierUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VehiclesType_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_VehiclesType_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpVehiclesRecord",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SBusID = c.Int(nullable: false),
                        SDevID = c.Int(nullable: false),
                        SUserNo = c.String(maxLength: 50),
                        SUserName = c.String(maxLength: 50),
                        SCarNo = c.String(maxLength: 50),
                        SAreaCardNo = c.String(maxLength: 50),
                        SInChnlIP = c.String(maxLength: 50),
                        SInLaneNo = c.String(maxLength: 50),
                        SDateBeg = c.String(maxLength: 50),
                        SInPicFile = c.String(maxLength: 50),
                        SOutChnlIP = c.String(maxLength: 50),
                        SOutLaneNo = c.String(maxLength: 50),
                        SDateEnd = c.String(maxLength: 50),
                        STotalSecs = c.Int(nullable: false),
                        SFreeSecs = c.Int(nullable: false),
                        SPaySecs = c.Int(nullable: false),
                        SCustID = c.Int(nullable: false),
                        SOutPicFile = c.String(maxLength: 50),
                        STypeName = c.String(maxLength: 50),
                        SAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SPayAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SFactAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SPayStatus = c.Int(nullable: false),
                        SInPicFileUrl = c.String(maxLength: 50),
                        SOutPicFileUrl = c.String(maxLength: 50),
                        SOutChnl = c.String(maxLength: 50),
                        SCarStyleID = c.Int(nullable: false),
                        SMemo = c.String(maxLength: 50),
                        SParkingName = c.String(maxLength: 50),
                        SOrigDevID = c.Int(nullable: false),
                        SExgWorkStatus = c.Int(nullable: false),
                        SExgWorkDate = c.String(maxLength: 50),
                        SBusIPAddr = c.String(maxLength: 50),
                        SOutDevID = c.Int(nullable: false),
                        SPlateNo = c.String(maxLength: 50),
                        SOutCarNo = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        LastModifierUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VehiclesRecord_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_VehiclesRecord_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpVehiclesPayType",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SRateTypeID = c.Int(nullable: false),
                        SRateTypeName = c.String(maxLength: 50),
                        SCarStyleID = c.Int(nullable: false),
                        SCarStyleName = c.String(maxLength: 50),
                        TenantId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        LastModifierUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VehiclesPayType_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_VehiclesPayType_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpVehiclesMonthCar",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SCustID = c.Int(nullable: false),
                        SUserNo = c.String(maxLength: 50),
                        SUserName = c.String(maxLength: 50),
                        SCarStyleID = c.Int(nullable: false),
                        SCardTypeID = c.Int(nullable: false),
                        SCardNo = c.String(maxLength: 50),
                        SAreaCardNo = c.String(maxLength: 50),
                        SPlateNo = c.String(maxLength: 50),
                        SCustName = c.String(maxLength: 50),
                        SLinkPhone = c.String(maxLength: 50),
                        SStartDate = c.String(maxLength: 50),
                        SEndDate = c.String(maxLength: 50),
                        SMemo = c.String(maxLength: 50),
                        SState = c.String(maxLength: 50),
                        TenantId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        LastModifierUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VehiclesMonthCar_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_VehiclesMonthCar_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpVehiclesHandMoveCutOff",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SRowID = c.Int(nullable: false),
                        SDate = c.String(maxLength: 50),
                        SMemo = c.String(maxLength: 50),
                        SUserNo = c.String(maxLength: 50),
                        SUserName = c.String(maxLength: 50),
                        STypeName = c.String(maxLength: 50),
                        SPlateNo = c.String(maxLength: 50),
                        SCustName = c.String(maxLength: 50),
                        SInLaneName = c.String(maxLength: 50),
                        SInLaneNo = c.String(maxLength: 50),
                        SOutLaneName = c.String(maxLength: 50),
                        SOutLaneNo = c.String(maxLength: 50),
                        SIsBackCar = c.String(maxLength: 50),
                        SInOutType = c.String(maxLength: 50),
                        TenantId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        LastModifierUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VehiclesHandMoveCutOff_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_VehiclesHandMoveCutOff_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpVehiclesFControlBarrierGate",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SId = c.Long(nullable: false),
                        SPicFile = c.String(maxLength: 50),
                        SMemo = c.String(maxLength: 50),
                        SDatetime = c.String(maxLength: 50),
                        SStatus = c.Int(nullable: false),
                        SStatusStr = c.String(maxLength: 50),
                        SInName = c.String(maxLength: 50),
                        SInChnlIP = c.String(maxLength: 50),
                        SOutChnlIP = c.String(maxLength: 50),
                        SInLaneName = c.String(maxLength: 50),
                        SInLaneNo = c.String(maxLength: 50),
                        SInDevStatus = c.Int(nullable: false),
                        SOutLaneName = c.String(maxLength: 50),
                        SOutLaneNo = c.String(maxLength: 50),
                        SOutDevStatus = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        LastModifierUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VehiclesFControlBarrierGate_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_VehiclesFControlBarrierGate_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpVehiclesExit",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SDevId = c.Int(nullable: false),
                        SInName = c.String(maxLength: 50),
                        SInChnlIP = c.String(maxLength: 50),
                        SOutChnlIP = c.String(maxLength: 50),
                        SDevMonDate = c.String(maxLength: 50),
                        SInLaneName = c.String(maxLength: 50),
                        SInLaneNo = c.String(maxLength: 50),
                        SInDevStatus = c.Int(nullable: false),
                        SOutLaneName = c.String(maxLength: 50),
                        SOutLaneNo = c.String(maxLength: 50),
                        SOutDevStatus = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        LastModifierUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VehiclesExit_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_VehiclesExit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpVehiclesError",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SID = c.Int(nullable: false),
                        SDate = c.String(maxLength: 50),
                        SMemo = c.String(maxLength: 50),
                        TenantId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        LastModifierUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VehiclesError_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_VehiclesError_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpVehiclesCalculator",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SPalteNoColor = c.String(maxLength: 50),
                        STotalSecs = c.Int(nullable: false),
                        SPaySecs = c.Int(nullable: false),
                        STotalAmount = c.Single(nullable: false),
                        SPayAmount = c.Single(nullable: false),
                        STotalFreeSecs = c.Int(nullable: false),
                        SBusID = c.Int(nullable: false),
                        SPlateNo = c.String(maxLength: 50),
                        SPlateNumber = c.String(maxLength: 50),
                        SAreaCardNo = c.String(maxLength: 50),
                        SInDateTime = c.String(maxLength: 50),
                        SPayDate = c.String(maxLength: 50),
                        SCustID = c.String(maxLength: 50),
                        SStatus = c.String(maxLength: 50),
                        SCardStatus = c.String(maxLength: 50),
                        STypeName = c.String(maxLength: 50),
                        SVouNoGroup = c.String(maxLength: 50),
                        SCarStyleID = c.String(maxLength: 50),
                        SCarStyleName = c.String(maxLength: 50),
                        TenantId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        LastModifierUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VehiclesCalculator_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_VehiclesCalculator_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpVehiclesBlackList",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SRowID = c.Int(nullable: false),
                        SPlateNo = c.String(maxLength: 50),
                        SDate = c.String(maxLength: 50),
                        SMemo = c.String(maxLength: 50),
                        SCarUserName = c.String(maxLength: 50),
                        SCarUserPhone = c.String(maxLength: 50),
                        SlimitDateBeg = c.String(maxLength: 50),
                        SlimitDateEnd = c.String(maxLength: 50),
                        TenantId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        LastModifierUserId = c.Long(),
                        LastModificationTime = c.DateTime(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VehiclesBlackList_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_VehiclesBlackList_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
