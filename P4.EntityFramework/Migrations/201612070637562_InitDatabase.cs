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
                "dbo.AbpAds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdImage = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpAppAccountRecords",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TransactionTime = c.DateTime(nullable: false),
                        AppAccountId = c.Long(nullable: false),
                        PlateNumber = c.String(maxLength: 10),
                        Remark = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpAppAccounts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserName = c.String(maxLength: 30),
                        Password = c.String(maxLength: 50),
                        Phone = c.String(maxLength: 20),
                        PhoneCode = c.String(maxLength: 10),
                        CodeTime = c.DateTime(nullable: false),
                        Wallet = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VersionNum = c.String(maxLength: 30),
                        IsLock = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        LastModificationTime = c.DateTime(),
                        PlateNumber1 = c.String(maxLength: 10),
                        Status1 = c.Boolean(nullable: false),
                        PlateNumber2 = c.String(maxLength: 10),
                        Status2 = c.Boolean(nullable: false),
                        PlateNumber3 = c.String(maxLength: 10),
                        Status3 = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpAuditLogs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        UserId = c.Long(),
                        ServiceName = c.String(maxLength: 256),
                        MethodName = c.String(maxLength: 256),
                        Parameters = c.String(maxLength: 1024),
                        ExecutionTime = c.DateTime(nullable: false),
                        ExecutionDuration = c.Int(nullable: false),
                        ClientIpAddress = c.String(maxLength: 64),
                        ClientName = c.String(maxLength: 128),
                        BrowserInfo = c.String(maxLength: 256),
                        Exception = c.String(maxLength: 2000),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AuditLog_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_AuditLog_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpBackgroundJobs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        JobType = c.String(nullable: false, maxLength: 512),
                        JobArgs = c.String(nullable: false),
                        TryCount = c.Short(nullable: false),
                        NextTryTime = c.DateTime(nullable: false),
                        LastTryTime = c.DateTime(),
                        IsAbandoned = c.Boolean(nullable: false),
                        Priority = c.Byte(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpBerths",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        BerthNumber = c.String(maxLength: 20),
                        BerthStatus = c.String(maxLength: 10),
                        RelateNumber = c.String(maxLength: 10),
                        CarType = c.Short(nullable: false),
                        InCarTime = c.DateTime(),
                        OutCarTime = c.DateTime(),
                        RegionId = c.Int(nullable: false),
                        ParkId = c.Int(nullable: false),
                        SensorNumber = c.String(maxLength: 15),
                        SensorsInCarTime = c.DateTime(),
                        SensorsOutCarTime = c.DateTime(),
                        ParkStatus = c.Short(),
                        SensorBeatTime = c.DateTime(),
                        BerthsecId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        guid = c.Guid(nullable: false),
                        SensorGuid = c.Guid(),
                        CardNo = c.String(maxLength: 20),
                        Prepaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Berth_MustHaveBerthsec", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Berth_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Berth_MustHavePark", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Berth_MustHaveRegion", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Berth_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpOperatorsCompany", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.AbpParks", t => t.ParkId, cascadeDelete: true)
                .ForeignKey("dbo.AbpTenants", t => t.TenantId, cascadeDelete: true)
                .Index(t => t.ParkId)
                .Index(t => t.TenantId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.AbpOperatorsCompany",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(maxLength: 200),
                        Corporation = c.String(maxLength: 20),
                        Contacts = c.String(maxLength: 20),
                        TelePhone = c.String(maxLength: 20),
                        Address = c.String(maxLength: 100),
                        TenantId = c.Int(nullable: false),
                        X = c.String(maxLength: 30),
                        Y = c.String(maxLength: 30),
                        IsActive = c.Boolean(nullable: false),
                        Password1 = c.String(maxLength: 20),
                        Password2 = c.String(maxLength: 20),
                        Password3 = c.String(maxLength: 20),
                        Password4 = c.String(maxLength: 20),
                        Password5 = c.String(maxLength: 20),
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
                    { "DynamicFilter_OperatorsCompany_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_OperatorsCompany_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpParks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegionId = c.Int(nullable: false),
                        ParkName = c.String(maxLength: 30),
                        ParkType = c.Short(nullable: false),
                        Mark = c.String(maxLength: 200),
                        BerthCount = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        X = c.String(maxLength: 30),
                        Y = c.String(maxLength: 30),
                        BeginNumber = c.String(maxLength: 10),
                        EndNumber = c.String(maxLength: 10),
                        OtherNumber = c.String(maxLength: 500),
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
                    { "DynamicFilter_Park_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Park_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Park_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("dbo.AbpUsers", t => t.LastModifierUserId)
                .ForeignKey("dbo.AbpRegions", t => t.RegionId, cascadeDelete: true)
                .Index(t => t.RegionId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.AbpUsers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        AuthenticationSource = c.String(maxLength: 64),
                        Name = c.String(nullable: false, maxLength: 32),
                        Surname = c.String(nullable: false, maxLength: 32),
                        Birthday = c.DateTime(),
                        Telephone = c.String(maxLength: 15),
                        UserName = c.String(nullable: false, maxLength: 32),
                        Password = c.String(nullable: false, maxLength: 128),
                        EmailAddress = c.String(nullable: false, maxLength: 256),
                        IsEmailConfirmed = c.Boolean(nullable: false),
                        EmailConfirmationCode = c.String(maxLength: 128),
                        PasswordResetCode = c.String(maxLength: 128),
                        LastLoginTime = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        Gender = c.Boolean(nullable: false),
                        CompanyId = c.Int(),
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
                    { "DynamicFilter_User_MayHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_User_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_User_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("dbo.AbpUsers", t => t.LastModifierUserId)
                .ForeignKey("dbo.AbpTenants", t => t.TenantId)
                .Index(t => t.TenantId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.AbpUserLogins",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AbpPermissions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        IsGranted = c.Boolean(nullable: false),
                        IsFunction = c.Boolean(nullable: false),
                        IsStatic = c.Boolean(nullable: false),
                        MultiTenancySide = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        UserId = c.Long(),
                        RoleId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AbpRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AbpUserRoles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        RoleId = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AbpSettings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        UserId = c.Long(),
                        Name = c.String(nullable: false, maxLength: 256),
                        Value = c.String(maxLength: 2000),
                        Mark = c.String(maxLength: 50),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.UserId)
                .ForeignKey("dbo.AbpTenants", t => t.TenantId)
                .Index(t => t.TenantId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AbpTenants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenancyName = c.String(nullable: false, maxLength: 64),
                        Name = c.String(nullable: false, maxLength: 128),
                        Sign = c.String(maxLength: 8),
                        IsActive = c.Boolean(nullable: false),
                        DomainName = c.String(maxLength: 50),
                        HQ = c.String(maxLength: 50),
                        Password = c.String(maxLength: 128),
                        Contacts = c.String(maxLength: 50),
                        EditionId = c.Int(),
                        Address = c.String(maxLength: 128),
                        Telephone = c.String(maxLength: 50),
                        X = c.String(maxLength: 30),
                        Y = c.String(maxLength: 30),
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
                    { "DynamicFilter_Tenant_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("dbo.AbpEditions", t => t.EditionId)
                .ForeignKey("dbo.AbpUsers", t => t.LastModifierUserId)
                .Index(t => t.EditionId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.AbpEditions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 32),
                        DisplayName = c.String(nullable: false, maxLength: 64),
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
                    { "DynamicFilter_Edition_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpRegions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FatherId = c.Int(nullable: false),
                        RegionName = c.String(maxLength: 30),
                        Mark = c.String(maxLength: 100),
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
                    { "DynamicFilter_Region_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Region_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Region_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("dbo.AbpUsers", t => t.LastModifierUserId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.AbpBerthsecReport",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        StopTime = c.Int(),
                        Prepaid = c.Decimal(precision: 18, scale: 2),
                        StopTimes = c.Int(nullable: false),
                        EscapeStopTimes = c.Int(nullable: false),
                        Receivable = c.Decimal(precision: 18, scale: 2),
                        Cash = c.Decimal(precision: 18, scale: 2),
                        FactReceive = c.Decimal(precision: 18, scale: 2),
                        Arrearage = c.Decimal(precision: 18, scale: 2),
                        Repayment = c.Decimal(precision: 18, scale: 2),
                        SensorsStopTime = c.Int(),
                        SensorsReceivable = c.Decimal(precision: 18, scale: 2),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        TenantId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        RegionId = c.Int(nullable: false),
                        ParkId = c.Int(nullable: false),
                        BerthsecId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BerthsecReport_MustHaveBerthsec", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BerthsecReport_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BerthsecReport_MustHavePark", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BerthsecReport_MustHaveRegion", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BerthsecReport_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpParks", t => t.ParkId, cascadeDelete: true)
                .Index(t => t.ParkId);
            
            CreateTable(
                "dbo.AbpBerthsecs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BerthsecName = c.String(maxLength: 38),
                        BeginNumeber = c.Int(nullable: false),
                        EndNumeber = c.Int(nullable: false),
                        CustomNumeber = c.String(maxLength: 500),
                        CheckInStatus = c.Boolean(nullable: false),
                        CheckStatus = c.Boolean(nullable: false),
                        CheckOutStatus = c.Boolean(nullable: false),
                        CheckInTime = c.DateTime(),
                        CheckInEmployeeId = c.Long(),
                        CheckOutTime = c.DateTime(),
                        CheckOutEmployeeId = c.Long(),
                        CheckInDeviceCode = c.String(maxLength: 40),
                        CheckOutDeviceCode = c.String(maxLength: 40),
                        XPoint = c.String(maxLength: 30),
                        YPoint = c.String(maxLength: 30),
                        RegionId = c.Int(nullable: false),
                        ParkId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        UseStatus = c.Boolean(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        RateId = c.Int(nullable: false),
                        BerthCount = c.String(maxLength: 30),
                        PushStatus = c.Boolean(nullable: false),
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
                    { "DynamicFilter_Berthsec_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Berthsec_MustHavePark", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Berthsec_MustHaveRegion", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Berthsec_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Berthsec_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
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
                        TenantId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        Version = c.Int(nullable: false),
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
                    { "DynamicFilter_BlackList_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BlackList_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BlackList_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpBusinessDetailPicture",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BusinessDetailId = c.Long(nullable: false),
                        BusinessDetailGuid = c.Guid(nullable: false),
                        BusinessDetailPictureUrl = c.String(maxLength: 500),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpBusinessDetail",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        BerthNumber = c.String(maxLength: 20),
                        PlateNumber = c.String(maxLength: 10),
                        CarType = c.Short(nullable: false),
                        Prepaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrepaidPayStatus = c.Short(nullable: false),
                        PrepaidCarNo = c.String(maxLength: 20),
                        Receivable = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Money = c.Decimal(precision: 18, scale: 2),
                        FactReceive = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Arrearage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BeforeDiscount = c.Decimal(precision: 18, scale: 2),
                        DiscountMoney = c.Decimal(precision: 18, scale: 2),
                        DiscountRate = c.Double(),
                        CarInTime = c.DateTime(),
                        CarOutTime = c.DateTime(),
                        CarPayTime = c.DateTime(),
                        InOperaId = c.Long(nullable: false),
                        InDeviceCode = c.String(maxLength: 40),
                        OutOperaId = c.Long(),
                        OutDeviceCode = c.String(maxLength: 40),
                        ChargeOperaId = c.Long(),
                        ChargeDeviceCode = c.String(maxLength: 40),
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
                        EscapeDeviceCode = c.String(maxLength: 40),
                        EscapeTenantId = c.Int(),
                        PayStatus = c.Short(nullable: false),
                        ReceivableCarNo = c.String(maxLength: 20),
                        OtherAccountId = c.Int(),
                        IsPay = c.Boolean(nullable: false),
                        StopType = c.Short(nullable: false),
                        FeeType = c.Short(nullable: false),
                        StopTime = c.Int(),
                        TenantId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        RegionId = c.Int(nullable: false),
                        ParkId = c.Int(nullable: false),
                        BerthsecId = c.Int(nullable: false),
                        Status = c.Short(nullable: false),
                        AppAccountId = c.Long(),
                        IsLock = c.Boolean(nullable: false),
                        EmployeeId = c.Long(),
                        ReceivableOtherAccountId = c.Long(),
                        EscapeCardNo = c.String(maxLength: 20),
                        EscapeOtherAccountId = c.Long(),
                        ElectronicOrderid = c.Long(),
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
                    { "DynamicFilter_BusinessDetail_MustHaveBerthsec", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BusinessDetail_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BusinessDetail_MustHavePark", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BusinessDetail_MustHaveRegion", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BusinessDetail_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BusinessDetail_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpBerthsecs", t => t.BerthsecId, cascadeDelete: true)
                .ForeignKey("dbo.AbpEmployees", t => t.ChargeOperaId)
                .ForeignKey("dbo.AbpEmployees", t => t.EscapeOperaId)
                .ForeignKey("dbo.AbpTenants", t => t.EscapeTenantId)
                .ForeignKey("dbo.AbpUsers", t => t.EscapeUserId)
                .ForeignKey("dbo.AbpEmployees", t => t.InOperaId, cascadeDelete: true)
                .ForeignKey("dbo.AbpEmployees", t => t.OutOperaId)
                .ForeignKey("dbo.AbpParks", t => t.ParkId, cascadeDelete: true)
                .ForeignKey("dbo.AbpTenants", t => t.TenantId, cascadeDelete: true)
                .Index(t => t.InOperaId)
                .Index(t => t.OutOperaId)
                .Index(t => t.ChargeOperaId)
                .Index(t => t.EscapeOperaId)
                .Index(t => t.EscapeUserId)
                .Index(t => t.EscapeTenantId)
                .Index(t => t.TenantId)
                .Index(t => t.ParkId)
                .Index(t => t.BerthsecId);
            
            CreateTable(
                "dbo.AbpEmployees",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserName = c.String(maxLength: 16),
                        TrueName = c.String(maxLength: 20),
                        BankCard = c.String(maxLength: 20),
                        AccountStatus = c.String(maxLength: 20),
                        Password = c.String(maxLength: 128),
                        Telephone = c.String(maxLength: 20),
                        IsPhoneConfirmed = c.Boolean(nullable: false),
                        CheckIn = c.Boolean(nullable: false),
                        CheckInTime = c.DateTime(),
                        DeviceCode = c.String(maxLength: 40),
                        CheckOut = c.Boolean(nullable: false),
                        CheckOuttype = c.String(maxLength: 10),
                        CheckOutUserId = c.Long(),
                        CheckOutTime = c.DateTime(),
                        Address = c.String(maxLength: 100),
                        TenantId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        RegionId = c.Int(nullable: false),
                        ParkId = c.Int(nullable: false),
                        BerthsecId = c.Int(nullable: false),
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
                    { "DynamicFilter_Employee_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Employee_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Employee_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CheckOutUserId)
                .Index(t => t.CheckOutUserId);
            
            CreateTable(
                "dbo.AbpCompanys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(maxLength: 200),
                        LoginCredentials = c.String(maxLength: 100),
                        X = c.String(maxLength: 30),
                        Y = c.String(maxLength: 30),
                        TelePhone = c.String(),
                        Address = c.String(),
                        Contacts = c.String(),
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
                    { "DynamicFilter_Company_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpDataLogItem",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DataLogId = c.Long(nullable: false),
                        Field = c.String(maxLength: 40),
                        FieldName = c.String(maxLength: 50),
                        OriginalValue = c.String(maxLength: 2000),
                        NewValue = c.String(maxLength: 2000),
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
            
            CreateTable(
                "dbo.AbpDataPermissions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CompanyIds = c.String(maxLength: 200),
                        RegionIds = c.String(maxLength: 1000),
                        ParkIds = c.String(maxLength: 2000),
                        BerthsecIds = c.String(maxLength: 3000),
                        TenantId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        RoleId = c.Int(),
                        UserId = c.Long(),
                        RoleId1 = c.Int(),
                        UserId1 = c.Long(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DataPermission_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_DataPermission_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_DataPermissionSetting_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_DataPermissionSetting_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_RoleDataPermissionSetting_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_RoleDataPermissionSetting_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_RoleDataPermissionSetting1_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_RoleDataPermissionSetting1_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_UserDataPermissionSetting_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_UserDataPermissionSetting_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_UserDataPermissionSetting1_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_UserDataPermissionSetting1_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpDeductionRecords",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        OtherAccountId = c.Long(nullable: false),
                        OperType = c.Short(nullable: false),
                        Money = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PayStatus = c.Boolean(nullable: false),
                        InTime = c.DateTime(nullable: false),
                        Remark = c.String(maxLength: 30),
                        CardNo = c.String(maxLength: 20),
                        EmployeeId = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                        PlateNumber = c.String(maxLength: 20),
                        TenantId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DeductionRecord_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_DeductionRecord_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExtOtherAccount", t => t.OtherAccountId, cascadeDelete: true)
                .Index(t => t.OtherAccountId);
            
            CreateTable(
                "dbo.ExtOtherAccount",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsEnabled = c.Boolean(nullable: false),
                        EnabledTime = c.DateTime(),
                        EnabledUserId = c.Long(),
                        CompanyId = c.Int(),
                        AuthenticationSource = c.String(maxLength: 64),
                        CardNo = c.String(maxLength: 20),
                        ProductNo = c.String(maxLength: 20),
                        Wallet = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Client = c.String(maxLength: 40),
                        UserName = c.String(maxLength: 20),
                        Name = c.String(maxLength: 30),
                        Password = c.String(maxLength: 30),
                        TelePhone = c.String(maxLength: 18),
                        IsPhoneConfirmed = c.Boolean(nullable: false),
                        AccountLoginDatetime = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsLock = c.Boolean(nullable: false),
                        PhoneCode = c.String(maxLength: 10),
                        CodeTime = c.DateTime(),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_OtherAccount_MayHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_OtherAccount_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_OtherAccount_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpEmployees", t => t.EnabledUserId)
                .Index(t => t.EnabledUserId);
            
            CreateTable(
                "dbo.AbpDictionaryType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeCode = c.String(maxLength: 20),
                        TypeValue = c.String(maxLength: 30),
                        TenantId = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        Remark = c.String(maxLength: 200),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DictionaryType_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpDictionaryTypeTemplate",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeCode = c.String(maxLength: 20),
                        TypeValue = c.String(maxLength: 30),
                        IsActive = c.Boolean(nullable: false),
                        TenantId = c.Int(),
                        IsDefault = c.Boolean(nullable: false),
                        Remark = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpDictionaryValue",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ValueCode = c.String(maxLength: 30),
                        TypeCode = c.String(maxLength: 20),
                        ValueData = c.String(maxLength: 50),
                        TenantId = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        Order = c.Int(nullable: false),
                        Remark = c.String(maxLength: 200),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DictionaryValue_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpDictionaryValueTemplate",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ValueCode = c.String(maxLength: 30),
                        TypeCode = c.String(maxLength: 20),
                        ValueData = c.String(maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        TenantId = c.Int(),
                        Order = c.Int(nullable: false),
                        Remark = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpFeatures",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Value = c.String(nullable: false, maxLength: 2000),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        EditionId = c.Int(),
                        TenantId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpEditions", t => t.EditionId, cascadeDelete: true)
                .Index(t => t.EditionId);
            
            CreateTable(
                "dbo.AbpEmployeeCheck",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EmployeeId = c.Long(nullable: false),
                        ParkId = c.Int(nullable: false),
                        CheckIn = c.Boolean(nullable: false),
                        CheckInTime = c.DateTime(),
                        CheckOut = c.Boolean(nullable: false),
                        CheckOutTime = c.DateTime(),
                        DeviceCode = c.String(maxLength: 40),
                        CheckOutUserId = c.Long(),
                        BerthsecId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_EmployeeCheck_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_EmployeeCheck_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpEmployees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.AbpUsers", t => t.CheckOutUserId)
                .Index(t => t.EmployeeId)
                .Index(t => t.CheckOutUserId);
            
            CreateTable(
                "dbo.AbpEmployeeReport",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        StopTime = c.Int(),
                        StopTimes = c.Int(nullable: false),
                        Prepaid = c.Decimal(precision: 18, scale: 2),
                        Receivable = c.Decimal(precision: 18, scale: 2),
                        FactReceive = c.Decimal(precision: 18, scale: 2),
                        Arrearage = c.Decimal(precision: 18, scale: 2),
                        Repayment = c.Decimal(precision: 18, scale: 2),
                        SensorsStopTime = c.Int(),
                        SensorsReceivable = c.Decimal(precision: 18, scale: 2),
                        EmployeeId = c.Long(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        TenantId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_EmployeeReport_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_EmployeeReport_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpEquipmentBeats",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PDA = c.String(maxLength: 50),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpEquipmentGps",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PDA = c.String(maxLength: 50),
                        X = c.String(maxLength: 30),
                        Y = c.String(maxLength: 30),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpEquipmentMaintenance",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EquipmentId = c.Long(nullable: false),
                        PDA = c.String(maxLength: 50),
                        UseStatus = c.Short(nullable: false),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        Ramark = c.String(maxLength: 300),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.AbpEquipments",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PDA = c.String(maxLength: 50),
                        Type = c.Short(nullable: false),
                        Sim = c.String(maxLength: 30),
                        SD = c.Boolean(nullable: false),
                        Pasm = c.String(maxLength: 30),
                        IsUpgrade = c.Boolean(nullable: false),
                        Pasm1 = c.String(maxLength: 30),
                        Printers = c.Boolean(nullable: false),
                        Version = c.String(maxLength: 20),
                        Standard = c.Short(nullable: false),
                        GPS = c.Boolean(nullable: false),
                        Scan = c.Boolean(nullable: false),
                        UseStatus = c.Short(nullable: false),
                        GetTime = c.DateTime(),
                        EmployeeId = c.Long(),
                        CompanyId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                        Remark = c.String(maxLength: 200),
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
                    { "DynamicFilter_Equipment_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Equipment_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Equipment_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("dbo.AbpUsers", t => t.LastModifierUserId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.AbpEquipmentVersions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantId = c.Int(nullable: false),
                        EqupmentType = c.String(maxLength: 5),
                        Version = c.String(maxLength: 30),
                        IsUpgrade = c.Boolean(nullable: false),
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
                    { "DynamicFilter_EquipmentVersion_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_EquipmentVersion_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpEscapeDetail",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        BerthNumber = c.String(maxLength: 20),
                        PlateNumber = c.String(maxLength: 10),
                        CarType = c.Short(nullable: false),
                        Prepaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrepaidPayStatus = c.Short(nullable: false),
                        PrepaidCarNo = c.String(),
                        Receivable = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Money = c.Decimal(precision: 18, scale: 2),
                        FactReceive = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Arrearage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CarInTime = c.DateTime(),
                        CarOutTime = c.DateTime(),
                        CarPayTime = c.DateTime(),
                        InOperaId = c.Long(nullable: false),
                        InDeviceCode = c.String(maxLength: 40),
                        OutOperaId = c.Long(),
                        OutDeviceCode = c.String(maxLength: 40),
                        ChargeOperaId = c.Long(),
                        ChargeDeviceCode = c.String(maxLength: 40),
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
                        EscapeDeviceCode = c.String(maxLength: 40),
                        EscapeTenantId = c.Int(),
                        PayStatus = c.Short(nullable: false),
                        ReceivableCarNo = c.String(),
                        OtherAccountId = c.Int(),
                        IsPay = c.Boolean(nullable: false),
                        StopType = c.Short(nullable: false),
                        FeeType = c.Short(nullable: false),
                        StopTime = c.Int(),
                        TenantId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        RegionId = c.Int(nullable: false),
                        ParkId = c.Int(nullable: false),
                        BerthsecId = c.Int(nullable: false),
                        Status = c.Short(nullable: false),
                        AppAccountId = c.Long(),
                        IsLock = c.Boolean(nullable: false),
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
                    { "DynamicFilter_EscapeDetail_MustHaveBerthsec", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_EscapeDetail_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_EscapeDetail_MustHavePark", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_EscapeDetail_MustHaveRegion", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_EscapeDetail_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_EscapeDetail_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpBerthsecs", t => t.BerthsecId, cascadeDelete: true)
                .ForeignKey("dbo.AbpEmployees", t => t.ChargeOperaId)
                .ForeignKey("dbo.AbpEmployees", t => t.EscapeOperaId)
                .ForeignKey("dbo.AbpTenants", t => t.EscapeTenantId)
                .ForeignKey("dbo.AbpUsers", t => t.EscapeUserId)
                .ForeignKey("dbo.AbpEmployees", t => t.InOperaId, cascadeDelete: true)
                .ForeignKey("dbo.AbpEmployees", t => t.OutOperaId)
                .ForeignKey("dbo.AbpParks", t => t.ParkId, cascadeDelete: true)
                .ForeignKey("dbo.AbpTenants", t => t.TenantId, cascadeDelete: true)
                .Index(t => t.InOperaId)
                .Index(t => t.OutOperaId)
                .Index(t => t.ChargeOperaId)
                .Index(t => t.EscapeOperaId)
                .Index(t => t.EscapeUserId)
                .Index(t => t.EscapeTenantId)
                .Index(t => t.TenantId)
                .Index(t => t.ParkId)
                .Index(t => t.BerthsecId);
            
            CreateTable(
                "dbo.AbpFeaturesLimitation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        DefaultValue = c.String(maxLength: 20),
                        Scope = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpIcons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IconUrl = c.String(maxLength: 50),
                        Remark = c.String(maxLength: 200),
                        Test = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpInducibleLogs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        InducibleId = c.Int(nullable: false),
                        EquipmentId = c.String(maxLength: 40),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpInducibles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InducibleName = c.String(maxLength: 50),
                        InducibleType = c.Short(nullable: false),
                        X = c.String(maxLength: 80),
                        Y = c.String(maxLength: 80),
                        Address = c.String(maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                        TenantId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        EquipmentId = c.String(maxLength: 40),
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
                    { "DynamicFilter_Inducible_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Inducible_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Inducible_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpInducibleToADs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InducibleId = c.Int(nullable: false),
                        AD = c.String(maxLength: 500),
                        BeginTime = c.DateTime(),
                        EndTime = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        EquipmentId = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpInducibles", t => t.InducibleId, cascadeDelete: true)
                .Index(t => t.InducibleId);
            
            CreateTable(
                "dbo.AbpInducibleToParks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InducibleId = c.Int(nullable: false),
                        ParkId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpParks", t => t.ParkId, cascadeDelete: true)
                .ForeignKey("dbo.AbpInducibles", t => t.InducibleId, cascadeDelete: true)
                .Index(t => t.InducibleId)
                .Index(t => t.ParkId);
            
            CreateTable(
                "dbo.AbpInspectorCheck",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        InspectorId = c.Long(nullable: false),
                        ParkId = c.Int(nullable: false),
                        CheckIn = c.Boolean(nullable: false),
                        CheckInTime = c.DateTime(),
                        CheckOut = c.Boolean(nullable: false),
                        CheckOutTime = c.DateTime(),
                        DeviceCode = c.String(maxLength: 40),
                        BerthsecId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_InspectorCheck_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_InspectorCheck_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpInspectors", t => t.InspectorId, cascadeDelete: true)
                .Index(t => t.InspectorId);
            
            CreateTable(
                "dbo.AbpInspectors",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserName = c.String(maxLength: 20),
                        TrueName = c.String(maxLength: 8),
                        BankCard = c.String(maxLength: 20),
                        AccountStatus = c.String(maxLength: 20),
                        CheckOuttype = c.String(maxLength: 10),
                        CheckOutUserId = c.Long(),
                        Password = c.String(maxLength: 128),
                        Telephone = c.String(maxLength: 20),
                        IsPhoneConfirmed = c.Boolean(nullable: false),
                        CheckIn = c.Boolean(nullable: false),
                        CheckInTime = c.DateTime(),
                        DeviceCode = c.String(maxLength: 40),
                        CheckOut = c.Boolean(nullable: false),
                        CheckOutTime = c.DateTime(),
                        Address = c.String(maxLength: 100),
                        CompanyId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                        RegionIds = c.String(maxLength: 500),
                        ParkIds = c.String(maxLength: 500),
                        BerthsecIds = c.String(maxLength: 1000),
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
                    { "DynamicFilter_Inspector_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Inspector_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Inspector_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CheckOutUserId)
                .Index(t => t.CheckOutUserId);
            
            CreateTable(
                "dbo.AbpInspectorTaskFeedbacks",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        InspectorTaskId = c.Long(nullable: false),
                        BerthsecId = c.Int(),
                        BerthNumber = c.String(maxLength: 200),
                        EmployeeId = c.Long(),
                        TaskContent = c.String(maxLength: 500),
                        UploadTime = c.DateTime(nullable: false),
                        Remark = c.String(maxLength: 500),
                        PicUrl1 = c.String(maxLength: 200),
                        PicUrl2 = c.String(maxLength: 200),
                        PicUrl3 = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpInspectorTasks",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ParkId = c.String(maxLength: 500),
                        EffectiveTime = c.DateTime(nullable: false),
                        InspectorId = c.Long(nullable: false),
                        Remark = c.String(maxLength: 500),
                        Status = c.Short(nullable: false),
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
                    { "DynamicFilter_InspectorTask_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_InspectorTask_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_InspectorTask_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpJobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobContent = c.String(maxLength: 20),
                        InTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpLanguages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 10),
                        DisplayName = c.String(nullable: false, maxLength: 64),
                        Icon = c.String(maxLength: 128),
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
                    { "DynamicFilter_ApplicationLanguage_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ApplicationLanguage_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpLanguageTexts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        LanguageName = c.String(nullable: false, maxLength: 10),
                        Source = c.String(nullable: false, maxLength: 128),
                        Key = c.String(nullable: false, maxLength: 256),
                        Value = c.String(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ApplicationLanguageText_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpMenus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 32),
                        FatherCode = c.String(maxLength: 30),
                        Level = c.Int(nullable: false),
                        LocalizableString = c.String(maxLength: 64),
                        Icon = c.String(maxLength: 200),
                        Url = c.String(maxLength: 200),
                        RequiresAuthentication = c.Boolean(nullable: false),
                        RequiredPermissionName = c.String(maxLength: 30),
                        Order = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        TenantId = c.Int(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        Discriminator = c.String(maxLength: 500),
                        CreatorUserId = c.Long(),
                        IsActive = c.Boolean(nullable: false),
                        IsStatic = c.Boolean(nullable: false),
                        IsFunction = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Menu_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Menu_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.AbpMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Msg = c.String(maxLength: 500),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        TenantId = c.Int(nullable: false),
                        ReviceUserId = c.Long(nullable: false),
                        ReadTime = c.DateTime(),
                        IsRead = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Message_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.AbpMonthlyCarAbnormal",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        MonthlyCarId = c.Int(nullable: false),
                        BusinessDetailId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Remark = c.String(maxLength: 200),
                        BerthNumber = c.String(maxLength: 20),
                        PlateNumber = c.String(maxLength: 10),
                        CarInTime = c.DateTime(nullable: false),
                        CarOutTime = c.DateTime(),
                        FactReceive = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsEscapePay = c.Boolean(nullable: false),
                        TenantId = c.Int(nullable: false),
                        BerthsecId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_MonthlyCarAbnormal_MustHaveBerthsec", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_MonthlyCarAbnormal_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpMonthlyCars", t => t.MonthlyCarId, cascadeDelete: true)
                .Index(t => t.MonthlyCarId);
            
            CreateTable(
                "dbo.AbpMonthlyCars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VehicleOwner = c.String(maxLength: 40),
                        Telphone = c.String(maxLength: 20),
                        PlateNumber = c.String(maxLength: 10),
                        Money = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ParkIds = c.String(maxLength: 200),
                        BeginTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        TenantId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        CarType = c.Int(nullable: false),
                        Version = c.Int(nullable: false),
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
                    { "DynamicFilter_MonthlyCar_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_MonthlyCar_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_MonthlyCar_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("dbo.AbpUsers", t => t.LastModifierUserId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.AbpMonthlyCarHistory",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        MonthlyCarId = c.Int(nullable: false),
                        Money = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ParkIds = c.String(maxLength: 200),
                        BeginTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.AbpNotices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NoticeInfo = c.String(maxLength: 2000),
                        NoticeUrl = c.String(maxLength: 100),
                        TenantId = c.Int(nullable: false),
                        CreatorUserId = c.Long(),
                        ReviceUserId = c.Long(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                        ReadTime = c.DateTime(),
                        CreationTime = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Notice_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                .Index(t => t.CreatorUserId);
            
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
            
            CreateTable(
                "dbo.AbpOperationPermissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        IsGrantedByDefault = c.Boolean(nullable: false),
                        Description = c.String(maxLength: 200),
                        MultiTenancySides = c.Short(nullable: false),
                        FatherCode = c.String(maxLength: 50),
                        IsFunction = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpOrganizations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrganizationCode = c.String(maxLength: 10),
                        OrganizationName = c.String(),
                        FatherCode = c.String(maxLength: 10),
                        TenantId = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                        CompanyId = c.Int(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Organization_MayHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Organization_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpOrganizationUnits",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        ParentId = c.Long(),
                        Code = c.String(nullable: false, maxLength: 128),
                        DisplayName = c.String(nullable: false, maxLength: 128),
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
                    { "DynamicFilter_OrganizationUnit_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_OrganizationUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpOrganizationUnits", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.ExtOtherPlateNumber",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AssignedOtherAccountId = c.Long(nullable: false),
                        PlateNumber = c.String(maxLength: 8),
                        CarColor = c.String(),
                        CarType = c.String(),
                        Order = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_OtherPlateNumber_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExtOtherAccount", t => t.AssignedOtherAccountId, cascadeDelete: true)
                .Index(t => t.AssignedOtherAccountId);
            
            CreateTable(
                "dbo.AbpPeakPeriod",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Utilization = c.Decimal(precision: 18, scale: 2),
                        BerthsecId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        BeginTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(),
                        Remark = c.String(maxLength: 200),
                        CreationTime = c.DateTime(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PeakPeriod_MustHaveBerthsec", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpRates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RateName = c.String(maxLength: 100),
                        RateJson = c.String(maxLength: 2000),
                        RatePDA = c.String(maxLength: 2000),
                        IsActive = c.Boolean(nullable: false),
                        Remark = c.String(maxLength: 200),
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
                    { "DynamicFilter_Rate_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Rate_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Rate_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("dbo.AbpUsers", t => t.LastModifierUserId)
                .ForeignKey("dbo.AbpOperatorsCompany", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.AbpTenants", t => t.TenantId, cascadeDelete: true)
                .Index(t => t.TenantId)
                .Index(t => t.CompanyId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.AbpRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 32),
                        DisplayName = c.String(nullable: false, maxLength: 64),
                        IsStatic = c.Boolean(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        CompanyId = c.Int(),
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
                    { "DynamicFilter_Role_MayHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Role_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Role_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("dbo.AbpUsers", t => t.LastModifierUserId)
                .ForeignKey("dbo.AbpTenants", t => t.TenantId)
                .Index(t => t.TenantId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.AbpSensorBeats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SensorCount = c.Int(nullable: false),
                        FaultCount = c.Int(nullable: false),
                        TenantId = c.Int(),
                        CreationTime = c.DateTime(nullable: false),
                        CompanyId = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SensorBeat_MayHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SensorBeat_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpSensorBusinessDetail",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        CompanyId = c.Int(),
                        RegionId = c.Int(),
                        ParkId = c.Int(),
                        BerthsecId = c.Int(),
                        BerthNumber = c.String(maxLength: 20),
                        SensorNumber = c.String(maxLength: 15),
                        Receivable = c.Decimal(precision: 18, scale: 2),
                        PlateNumber = c.String(maxLength: 10),
                        CarInTime = c.DateTime(),
                        CarOutTime = c.DateTime(),
                        StopTime = c.Int(),
                        guid = c.Guid(nullable: false),
                        Indicate = c.String(maxLength: 10),
                        CreationTime = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpSensorCaution",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SensorId = c.Int(nullable: false),
                        Magnetism = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Battery = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpSensors", t => t.SensorId, cascadeDelete: true)
                .Index(t => t.SensorId);
            
            CreateTable(
                "dbo.AbpSensors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantId = c.Int(),
                        CompanyId = c.Int(),
                        RegionId = c.Int(),
                        ParkId = c.Int(),
                        BerthsecId = c.Int(),
                        BerthNumber = c.String(maxLength: 20),
                        RelateNumber = c.String(maxLength: 10),
                        InCarTime = c.DateTime(),
                        OutCarTime = c.DateTime(),
                        Receivable = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ParkStatus = c.Short(nullable: false),
                        Magnetism = c.Decimal(precision: 18, scale: 2),
                        Battery = c.Decimal(precision: 18, scale: 2),
                        Updatetime = c.DateTime(),
                        TransmitterNumber = c.String(maxLength: 12),
                        SensorNumber = c.String(maxLength: 15),
                        X = c.Int(),
                        Y = c.Int(),
                        Z = c.Int(),
                        Range = c.String(maxLength: 20),
                        CreationTime = c.DateTime(nullable: false),
                        BeatDatetime = c.DateTime(),
                        Guid = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpBerthsecs", t => t.BerthsecId)
                .Index(t => t.BerthsecId);
            
            CreateTable(
                "dbo.AbpSensorFaults",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SensorBeatId = c.Int(nullable: false),
                        SensorNumber = c.String(maxLength: 15),
                        CreationTime = c.DateTime(nullable: false),
                        RegionId = c.Int(),
                        ParkId = c.Int(),
                        BerthsecId = c.Int(),
                        BerthNumber = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpBerthsecs", t => t.BerthsecId)
                .ForeignKey("dbo.AbpParks", t => t.ParkId)
                .ForeignKey("dbo.AbpRegions", t => t.RegionId)
                .Index(t => t.RegionId)
                .Index(t => t.ParkId)
                .Index(t => t.BerthsecId);
            
            CreateTable(
                "dbo.AbpSensorLogs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Content = c.String(maxLength: 2048),
                        Exception = c.String(maxLength: 500),
                        ReceiptCmd = c.String(maxLength: 100),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpSensorsMagnetic",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SensorNumber = c.String(maxLength: 10),
                        Number = c.Int(nullable: false),
                        Voltage = c.Int(nullable: false),
                        Signal = c.Int(nullable: false),
                        X = c.Int(nullable: false),
                        Y = c.Int(nullable: false),
                        Z = c.Int(nullable: false),
                        X0 = c.Int(),
                        Y0 = c.Int(),
                        Z0 = c.Int(),
                        Variance = c.Double(nullable: false),
                        Status = c.Short(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpSensorRunStatus",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SensorNumber = c.String(maxLength: 15),
                        BeginTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(),
                        Duration = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpSettingTemplate",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantId = c.Int(),
                        UserId = c.Long(),
                        Name = c.String(nullable: false, maxLength: 256),
                        Value = c.String(maxLength: 2000),
                        Mark = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpSms",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SmsMsg = c.String(maxLength: 2000),
                        TelePhones = c.String(maxLength: 200),
                        SmsResult = c.String(maxLength: 500),
                        CreationTime = c.DateTime(nullable: false),
                        SmsCount = c.Int(nullable: false),
                        TenantId = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Sms_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpSpecialList",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VehicleType = c.String(maxLength: 50),
                        PlateNumber = c.String(maxLength: 50),
                        IdNumber = c.String(maxLength: 50),
                        CarOwner = c.String(maxLength: 50),
                        Remarks = c.String(maxLength: 500),
                        Discount = c.String(maxLength: 10),
                        IsActive = c.Boolean(nullable: false),
                        TenantId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        SpecilType = c.Int(),
                        BeginDiscountTime = c.String(),
                        EndDiscountTime = c.String(),
                        ParkId = c.Int(nullable: false),
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
                    { "DynamicFilter_SpecialList_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SpecialList_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SpecialList_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TaskInfo = c.String(maxLength: 500),
                        ReadUserId = c.Long(nullable: false),
                        ReadTime = c.DateTime(),
                        IsRead = c.Boolean(nullable: false),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        TenantId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Task_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.AbpTransmitterCaution",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TransmitterId = c.Int(nullable: false),
                        VoltageCaution = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpTransmitters", t => t.TransmitterId, cascadeDelete: true)
                .Index(t => t.TransmitterId);
            
            CreateTable(
                "dbo.AbpTransmitters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransmitterName = c.String(maxLength: 50),
                        TransmitterNumber = c.String(nullable: false, maxLength: 12),
                        VoltageCaution = c.Decimal(precision: 18, scale: 2),
                        Updatetime = c.DateTime(),
                        X = c.String(maxLength: 50),
                        Y = c.String(maxLength: 50),
                        Address = c.String(maxLength: 30),
                        CreationTime = c.DateTime(nullable: false),
                        BeatDatetime = c.DateTime(),
                        TenantId = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Transmitter_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpUserOrganizationUnits",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        UserId = c.Long(nullable: false),
                        OrganizationUnitId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserOrganizationUnit_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpUserTrials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(maxLength: 50),
                        TrueName = c.String(maxLength: 10),
                        CompanyName = c.String(maxLength: 30),
                        Telephone = c.String(maxLength: 18),
                        Address = c.String(maxLength: 30),
                        CreationTime = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpWhiteList",
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
                        Version = c.Int(nullable: false),
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
                    { "DynamicFilter_WhiteList_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_WhiteList_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_WhiteList_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpWorkGroupBerthsecs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BerthsecId = c.Int(nullable: false),
                        WorkGroupId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
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
                    { "DynamicFilter_WorkGroupBerthsec_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_WorkGroupBerthsec_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_WorkGroupBerthsec_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpBerthsecs", t => t.BerthsecId, cascadeDelete: true)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("dbo.AbpUsers", t => t.LastModifierUserId)
                .ForeignKey("dbo.AbpWorkGroups", t => t.WorkGroupId, cascadeDelete: true)
                .Index(t => t.BerthsecId)
                .Index(t => t.WorkGroupId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.AbpWorkGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkGroupName = c.String(maxLength: 50),
                        TenantId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
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
                    { "DynamicFilter_WorkGroup_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_WorkGroup_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_WorkGroup_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("dbo.AbpUsers", t => t.LastModifierUserId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.AbpWorkGroupEmployees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkGroupId = c.Int(nullable: false),
                        EmployeeId = c.Long(nullable: false),
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
                    { "DynamicFilter_WorkGroupEmployee_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("dbo.AbpEmployees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.AbpUsers", t => t.LastModifierUserId)
                .ForeignKey("dbo.AbpWorkGroups", t => t.WorkGroupId, cascadeDelete: true)
                .Index(t => t.WorkGroupId)
                .Index(t => t.EmployeeId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AbpWorkGroupEmployees", "WorkGroupId", "dbo.AbpWorkGroups");
            DropForeignKey("dbo.AbpWorkGroupEmployees", "LastModifierUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpWorkGroupEmployees", "EmployeeId", "dbo.AbpEmployees");
            DropForeignKey("dbo.AbpWorkGroupEmployees", "DeleterUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpWorkGroupEmployees", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpWorkGroupBerthsecs", "WorkGroupId", "dbo.AbpWorkGroups");
            DropForeignKey("dbo.AbpWorkGroups", "LastModifierUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpWorkGroups", "DeleterUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpWorkGroups", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpWorkGroupBerthsecs", "LastModifierUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpWorkGroupBerthsecs", "DeleterUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpWorkGroupBerthsecs", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpWorkGroupBerthsecs", "BerthsecId", "dbo.AbpBerthsecs");
            DropForeignKey("dbo.AbpTransmitterCaution", "TransmitterId", "dbo.AbpTransmitters");
            DropForeignKey("dbo.AbpTasks", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpSensorFaults", "RegionId", "dbo.AbpRegions");
            DropForeignKey("dbo.AbpSensorFaults", "ParkId", "dbo.AbpParks");
            DropForeignKey("dbo.AbpSensorFaults", "BerthsecId", "dbo.AbpBerthsecs");
            DropForeignKey("dbo.AbpSensorCaution", "SensorId", "dbo.AbpSensors");
            DropForeignKey("dbo.AbpSensors", "BerthsecId", "dbo.AbpBerthsecs");
            DropForeignKey("dbo.AbpRoles", "TenantId", "dbo.AbpTenants");
            DropForeignKey("dbo.AbpPermissions", "RoleId", "dbo.AbpRoles");
            DropForeignKey("dbo.AbpRoles", "LastModifierUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpRoles", "DeleterUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpRoles", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpRates", "TenantId", "dbo.AbpTenants");
            DropForeignKey("dbo.AbpRates", "CompanyId", "dbo.AbpOperatorsCompany");
            DropForeignKey("dbo.AbpRates", "LastModifierUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpRates", "DeleterUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpRates", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.ExtOtherPlateNumber", "AssignedOtherAccountId", "dbo.ExtOtherAccount");
            DropForeignKey("dbo.AbpOrganizationUnits", "ParentId", "dbo.AbpOrganizationUnits");
            DropForeignKey("dbo.AbpNotices", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpMonthlyCarHistory", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpMonthlyCarAbnormal", "MonthlyCarId", "dbo.AbpMonthlyCars");
            DropForeignKey("dbo.AbpMonthlyCars", "LastModifierUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpMonthlyCars", "DeleterUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpMonthlyCars", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpMessages", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpMenus", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpInspectorCheck", "InspectorId", "dbo.AbpInspectors");
            DropForeignKey("dbo.AbpInspectors", "CheckOutUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpInducibleToParks", "InducibleId", "dbo.AbpInducibles");
            DropForeignKey("dbo.AbpInducibleToParks", "ParkId", "dbo.AbpParks");
            DropForeignKey("dbo.AbpInducibleToADs", "InducibleId", "dbo.AbpInducibles");
            DropForeignKey("dbo.AbpEscapeDetail", "TenantId", "dbo.AbpTenants");
            DropForeignKey("dbo.AbpEscapeDetail", "ParkId", "dbo.AbpParks");
            DropForeignKey("dbo.AbpEscapeDetail", "OutOperaId", "dbo.AbpEmployees");
            DropForeignKey("dbo.AbpEscapeDetail", "InOperaId", "dbo.AbpEmployees");
            DropForeignKey("dbo.AbpEscapeDetail", "EscapeUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpEscapeDetail", "EscapeTenantId", "dbo.AbpTenants");
            DropForeignKey("dbo.AbpEscapeDetail", "EscapeOperaId", "dbo.AbpEmployees");
            DropForeignKey("dbo.AbpEscapeDetail", "ChargeOperaId", "dbo.AbpEmployees");
            DropForeignKey("dbo.AbpEscapeDetail", "BerthsecId", "dbo.AbpBerthsecs");
            DropForeignKey("dbo.AbpEquipments", "LastModifierUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpEquipments", "DeleterUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpEquipments", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpEquipmentMaintenance", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpEmployeeCheck", "CheckOutUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpEmployeeCheck", "EmployeeId", "dbo.AbpEmployees");
            DropForeignKey("dbo.AbpFeatures", "EditionId", "dbo.AbpEditions");
            DropForeignKey("dbo.AbpDeductionRecords", "OtherAccountId", "dbo.ExtOtherAccount");
            DropForeignKey("dbo.ExtOtherAccount", "EnabledUserId", "dbo.AbpEmployees");
            DropForeignKey("dbo.AbpDataLogItem", "DataLogId", "dbo.AbpDatalogs");
            DropForeignKey("dbo.AbpBusinessDetail", "TenantId", "dbo.AbpTenants");
            DropForeignKey("dbo.AbpBusinessDetail", "ParkId", "dbo.AbpParks");
            DropForeignKey("dbo.AbpBusinessDetail", "OutOperaId", "dbo.AbpEmployees");
            DropForeignKey("dbo.AbpBusinessDetail", "InOperaId", "dbo.AbpEmployees");
            DropForeignKey("dbo.AbpBusinessDetail", "EscapeUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpBusinessDetail", "EscapeTenantId", "dbo.AbpTenants");
            DropForeignKey("dbo.AbpBusinessDetail", "EscapeOperaId", "dbo.AbpEmployees");
            DropForeignKey("dbo.AbpBusinessDetail", "ChargeOperaId", "dbo.AbpEmployees");
            DropForeignKey("dbo.AbpEmployees", "CheckOutUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpBusinessDetail", "BerthsecId", "dbo.AbpBerthsecs");
            DropForeignKey("dbo.AbpBerthsecReport", "ParkId", "dbo.AbpParks");
            DropForeignKey("dbo.AbpBerths", "TenantId", "dbo.AbpTenants");
            DropForeignKey("dbo.AbpBerths", "ParkId", "dbo.AbpParks");
            DropForeignKey("dbo.AbpParks", "RegionId", "dbo.AbpRegions");
            DropForeignKey("dbo.AbpRegions", "LastModifierUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpRegions", "DeleterUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpRegions", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpParks", "LastModifierUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpParks", "DeleterUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpParks", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUsers", "TenantId", "dbo.AbpTenants");
            DropForeignKey("dbo.AbpSettings", "TenantId", "dbo.AbpTenants");
            DropForeignKey("dbo.AbpTenants", "LastModifierUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpTenants", "EditionId", "dbo.AbpEditions");
            DropForeignKey("dbo.AbpTenants", "DeleterUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpTenants", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpSettings", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUserRoles", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpPermissions", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUserLogins", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUsers", "LastModifierUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUsers", "DeleterUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUsers", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpBerths", "CompanyId", "dbo.AbpOperatorsCompany");
            DropIndex("dbo.AbpWorkGroupEmployees", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpWorkGroupEmployees", new[] { "LastModifierUserId" });
            DropIndex("dbo.AbpWorkGroupEmployees", new[] { "DeleterUserId" });
            DropIndex("dbo.AbpWorkGroupEmployees", new[] { "EmployeeId" });
            DropIndex("dbo.AbpWorkGroupEmployees", new[] { "WorkGroupId" });
            DropIndex("dbo.AbpWorkGroups", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpWorkGroups", new[] { "LastModifierUserId" });
            DropIndex("dbo.AbpWorkGroups", new[] { "DeleterUserId" });
            DropIndex("dbo.AbpWorkGroupBerthsecs", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpWorkGroupBerthsecs", new[] { "LastModifierUserId" });
            DropIndex("dbo.AbpWorkGroupBerthsecs", new[] { "DeleterUserId" });
            DropIndex("dbo.AbpWorkGroupBerthsecs", new[] { "WorkGroupId" });
            DropIndex("dbo.AbpWorkGroupBerthsecs", new[] { "BerthsecId" });
            DropIndex("dbo.AbpTransmitterCaution", new[] { "TransmitterId" });
            DropIndex("dbo.AbpTasks", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpSensorFaults", new[] { "BerthsecId" });
            DropIndex("dbo.AbpSensorFaults", new[] { "ParkId" });
            DropIndex("dbo.AbpSensorFaults", new[] { "RegionId" });
            DropIndex("dbo.AbpSensors", new[] { "BerthsecId" });
            DropIndex("dbo.AbpSensorCaution", new[] { "SensorId" });
            DropIndex("dbo.AbpRoles", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpRoles", new[] { "LastModifierUserId" });
            DropIndex("dbo.AbpRoles", new[] { "DeleterUserId" });
            DropIndex("dbo.AbpRoles", new[] { "TenantId" });
            DropIndex("dbo.AbpRates", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpRates", new[] { "LastModifierUserId" });
            DropIndex("dbo.AbpRates", new[] { "DeleterUserId" });
            DropIndex("dbo.AbpRates", new[] { "CompanyId" });
            DropIndex("dbo.AbpRates", new[] { "TenantId" });
            DropIndex("dbo.ExtOtherPlateNumber", new[] { "AssignedOtherAccountId" });
            DropIndex("dbo.AbpOrganizationUnits", new[] { "ParentId" });
            DropIndex("dbo.AbpNotices", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpMonthlyCarHistory", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpMonthlyCars", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpMonthlyCars", new[] { "LastModifierUserId" });
            DropIndex("dbo.AbpMonthlyCars", new[] { "DeleterUserId" });
            DropIndex("dbo.AbpMonthlyCarAbnormal", new[] { "MonthlyCarId" });
            DropIndex("dbo.AbpMessages", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpMenus", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpInspectors", new[] { "CheckOutUserId" });
            DropIndex("dbo.AbpInspectorCheck", new[] { "InspectorId" });
            DropIndex("dbo.AbpInducibleToParks", new[] { "ParkId" });
            DropIndex("dbo.AbpInducibleToParks", new[] { "InducibleId" });
            DropIndex("dbo.AbpInducibleToADs", new[] { "InducibleId" });
            DropIndex("dbo.AbpEscapeDetail", new[] { "BerthsecId" });
            DropIndex("dbo.AbpEscapeDetail", new[] { "ParkId" });
            DropIndex("dbo.AbpEscapeDetail", new[] { "TenantId" });
            DropIndex("dbo.AbpEscapeDetail", new[] { "EscapeTenantId" });
            DropIndex("dbo.AbpEscapeDetail", new[] { "EscapeUserId" });
            DropIndex("dbo.AbpEscapeDetail", new[] { "EscapeOperaId" });
            DropIndex("dbo.AbpEscapeDetail", new[] { "ChargeOperaId" });
            DropIndex("dbo.AbpEscapeDetail", new[] { "OutOperaId" });
            DropIndex("dbo.AbpEscapeDetail", new[] { "InOperaId" });
            DropIndex("dbo.AbpEquipments", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpEquipments", new[] { "LastModifierUserId" });
            DropIndex("dbo.AbpEquipments", new[] { "DeleterUserId" });
            DropIndex("dbo.AbpEquipmentMaintenance", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpEmployeeCheck", new[] { "CheckOutUserId" });
            DropIndex("dbo.AbpEmployeeCheck", new[] { "EmployeeId" });
            DropIndex("dbo.AbpFeatures", new[] { "EditionId" });
            DropIndex("dbo.ExtOtherAccount", new[] { "EnabledUserId" });
            DropIndex("dbo.AbpDeductionRecords", new[] { "OtherAccountId" });
            DropIndex("dbo.AbpDataLogItem", new[] { "DataLogId" });
            DropIndex("dbo.AbpEmployees", new[] { "CheckOutUserId" });
            DropIndex("dbo.AbpBusinessDetail", new[] { "BerthsecId" });
            DropIndex("dbo.AbpBusinessDetail", new[] { "ParkId" });
            DropIndex("dbo.AbpBusinessDetail", new[] { "TenantId" });
            DropIndex("dbo.AbpBusinessDetail", new[] { "EscapeTenantId" });
            DropIndex("dbo.AbpBusinessDetail", new[] { "EscapeUserId" });
            DropIndex("dbo.AbpBusinessDetail", new[] { "EscapeOperaId" });
            DropIndex("dbo.AbpBusinessDetail", new[] { "ChargeOperaId" });
            DropIndex("dbo.AbpBusinessDetail", new[] { "OutOperaId" });
            DropIndex("dbo.AbpBusinessDetail", new[] { "InOperaId" });
            DropIndex("dbo.AbpBerthsecReport", new[] { "ParkId" });
            DropIndex("dbo.AbpRegions", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpRegions", new[] { "LastModifierUserId" });
            DropIndex("dbo.AbpRegions", new[] { "DeleterUserId" });
            DropIndex("dbo.AbpTenants", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpTenants", new[] { "LastModifierUserId" });
            DropIndex("dbo.AbpTenants", new[] { "DeleterUserId" });
            DropIndex("dbo.AbpTenants", new[] { "EditionId" });
            DropIndex("dbo.AbpSettings", new[] { "UserId" });
            DropIndex("dbo.AbpSettings", new[] { "TenantId" });
            DropIndex("dbo.AbpUserRoles", new[] { "UserId" });
            DropIndex("dbo.AbpPermissions", new[] { "RoleId" });
            DropIndex("dbo.AbpPermissions", new[] { "UserId" });
            DropIndex("dbo.AbpUserLogins", new[] { "UserId" });
            DropIndex("dbo.AbpUsers", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpUsers", new[] { "LastModifierUserId" });
            DropIndex("dbo.AbpUsers", new[] { "DeleterUserId" });
            DropIndex("dbo.AbpUsers", new[] { "TenantId" });
            DropIndex("dbo.AbpParks", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpParks", new[] { "LastModifierUserId" });
            DropIndex("dbo.AbpParks", new[] { "DeleterUserId" });
            DropIndex("dbo.AbpParks", new[] { "RegionId" });
            DropIndex("dbo.AbpBerths", new[] { "CompanyId" });
            DropIndex("dbo.AbpBerths", new[] { "TenantId" });
            DropIndex("dbo.AbpBerths", new[] { "ParkId" });
            DropTable("dbo.AbpWorkGroupEmployees",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_WorkGroupEmployee_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpWorkGroups",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_WorkGroup_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_WorkGroup_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_WorkGroup_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpWorkGroupBerthsecs",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_WorkGroupBerthsec_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_WorkGroupBerthsec_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_WorkGroupBerthsec_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpWhiteList",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_WhiteList_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_WhiteList_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_WhiteList_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpUserTrials");
            DropTable("dbo.AbpUserOrganizationUnits",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserOrganizationUnit_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpTransmitters",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Transmitter_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpTransmitterCaution");
            DropTable("dbo.AbpTasks",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Task_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpSpecialList",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SpecialList_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SpecialList_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SpecialList_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpSms",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Sms_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpSettingTemplate");
            DropTable("dbo.AbpSensorRunStatus");
            DropTable("dbo.AbpSensorsMagnetic");
            DropTable("dbo.AbpSensorLogs");
            DropTable("dbo.AbpSensorFaults");
            DropTable("dbo.AbpSensors");
            DropTable("dbo.AbpSensorCaution");
            DropTable("dbo.AbpSensorBusinessDetail");
            DropTable("dbo.AbpSensorBeats",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SensorBeat_MayHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SensorBeat_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpRoles",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Role_MayHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Role_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Role_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpRates",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Rate_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Rate_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Rate_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpPeakPeriod",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PeakPeriod_MustHaveBerthsec", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.ExtOtherPlateNumber",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_OtherPlateNumber_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpOrganizationUnits",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_OrganizationUnit_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_OrganizationUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpOrganizations",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Organization_MayHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Organization_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpOperationPermissions");
            DropTable("dbo.AbpOperationLogs",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_OperationLog_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpNotices",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Notice_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpMonthlyCarHistory");
            DropTable("dbo.AbpMonthlyCars",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_MonthlyCar_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_MonthlyCar_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_MonthlyCar_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpMonthlyCarAbnormal",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_MonthlyCarAbnormal_MustHaveBerthsec", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_MonthlyCarAbnormal_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpMessages",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Message_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpMenus",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Menu_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Menu_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpLanguageTexts",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ApplicationLanguageText_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpLanguages",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ApplicationLanguage_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ApplicationLanguage_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpJobs");
            DropTable("dbo.AbpInspectorTasks",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_InspectorTask_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_InspectorTask_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_InspectorTask_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpInspectorTaskFeedbacks");
            DropTable("dbo.AbpInspectors",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Inspector_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Inspector_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Inspector_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpInspectorCheck",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_InspectorCheck_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_InspectorCheck_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpInducibleToParks");
            DropTable("dbo.AbpInducibleToADs");
            DropTable("dbo.AbpInducibles",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Inducible_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Inducible_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Inducible_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpInducibleLogs");
            DropTable("dbo.AbpIcons");
            DropTable("dbo.AbpFeaturesLimitation");
            DropTable("dbo.AbpEscapeDetail",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_EscapeDetail_MustHaveBerthsec", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_EscapeDetail_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_EscapeDetail_MustHavePark", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_EscapeDetail_MustHaveRegion", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_EscapeDetail_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_EscapeDetail_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpEquipmentVersions",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_EquipmentVersion_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_EquipmentVersion_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpEquipments",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Equipment_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Equipment_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Equipment_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpEquipmentMaintenance");
            DropTable("dbo.AbpEquipmentGps");
            DropTable("dbo.AbpEquipmentBeats");
            DropTable("dbo.AbpEmployeeReport",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_EmployeeReport_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_EmployeeReport_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpEmployeeCheck",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_EmployeeCheck_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_EmployeeCheck_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpFeatures");
            DropTable("dbo.AbpDictionaryValueTemplate");
            DropTable("dbo.AbpDictionaryValue",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DictionaryValue_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpDictionaryTypeTemplate");
            DropTable("dbo.AbpDictionaryType",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DictionaryType_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.ExtOtherAccount",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_OtherAccount_MayHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_OtherAccount_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_OtherAccount_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpDeductionRecords",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DeductionRecord_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_DeductionRecord_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpDataPermissions",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DataPermission_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_DataPermission_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_DataPermissionSetting_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_DataPermissionSetting_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_RoleDataPermissionSetting_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_RoleDataPermissionSetting_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_RoleDataPermissionSetting1_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_RoleDataPermissionSetting1_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_UserDataPermissionSetting_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_UserDataPermissionSetting_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_UserDataPermissionSetting1_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_UserDataPermissionSetting1_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpDatalogs",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Datalog_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpDataLogItem");
            DropTable("dbo.AbpCompanys",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Company_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpEmployees",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Employee_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Employee_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Employee_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpBusinessDetail",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BusinessDetail_MustHaveBerthsec", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BusinessDetail_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BusinessDetail_MustHavePark", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BusinessDetail_MustHaveRegion", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BusinessDetail_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BusinessDetail_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpBusinessDetailPicture");
            DropTable("dbo.AbpBlackList",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BlackList_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BlackList_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BlackList_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpBerthsecs",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Berthsec_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Berthsec_MustHavePark", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Berthsec_MustHaveRegion", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Berthsec_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Berthsec_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpBerthsecReport",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BerthsecReport_MustHaveBerthsec", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BerthsecReport_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BerthsecReport_MustHavePark", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BerthsecReport_MustHaveRegion", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BerthsecReport_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpRegions",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Region_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Region_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Region_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpEditions",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Edition_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpTenants",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Tenant_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpSettings");
            DropTable("dbo.AbpUserRoles");
            DropTable("dbo.AbpPermissions");
            DropTable("dbo.AbpUserLogins");
            DropTable("dbo.AbpUsers",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_User_MayHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_User_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_User_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpParks",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Park_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Park_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Park_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpOperatorsCompany",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_OperatorsCompany_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_OperatorsCompany_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpBerths",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Berth_MustHaveBerthsec", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Berth_MustHaveCompany", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Berth_MustHavePark", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Berth_MustHaveRegion", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Berth_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpBackgroundJobs");
            DropTable("dbo.AbpAuditLogs",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AuditLog_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_AuditLog_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpAppAccounts");
            DropTable("dbo.AbpAppAccountRecords");
            DropTable("dbo.AbpAds");
        }
    }
}
