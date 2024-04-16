namespace P4.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class intidatebase : DbMigration
    {
        public override void Up()
        {
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
                "dbo.AbpBerths",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        BerthNumber = c.String(),
                        BerthStatus = c.Boolean(nullable: false),
                        RegionId = c.Int(nullable: false),
                        ParkId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        BerthsecId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Berth_MustHaveBerthsec", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Berth_MustHavePark", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Berth_MustHaveRegion", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Berth_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpBerthsecs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BerthsecName = c.String(),
                        Status = c.Boolean(nullable: false),
                        CheckInTime = c.DateTime(nullable: false),
                        CheckInName = c.String(maxLength: 20),
                        CheckOutTime = c.DateTime(nullable: false),
                        CheckOutName = c.String(maxLength: 20),
                        XPoint = c.String(),
                        YPoint = c.String(),
                        RegionId = c.Int(nullable: false),
                        ParkId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
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
                    { "DynamicFilter_Berthsec_MustHavePark", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Berthsec_MustHaveRegion", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Berthsec_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Berthsec_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpCompanys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(maxLength: 200),
                        LoginCredentials = c.String(maxLength: 100),
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
                "dbo.AbpDataPermissions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        UserId = c.Long(nullable: false),
                        RegionIds = c.String(),
                        ParkIds = c.String(),
                        BerthsecIds = c.String(),
                        TenantId = c.Int(nullable: false),
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
                    { "DynamicFilter_DataPermission_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_DataPermission_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpDictionaryType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeCode = c.String(maxLength: 20),
                        TypeValue = c.String(maxLength: 30),
                        TenantId = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        Remark = c.String(maxLength: 200),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DictionaryType_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpDictionaryValue",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ValueCode = c.String(maxLength: 20),
                        TypeCode = c.String(maxLength: 20),
                        ValueData = c.String(maxLength: 50),
                        TenantId = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        Remark = c.String(maxLength: 200),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DictionaryValue_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpIcons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IconUrl = c.String(),
                        Remark = c.String(),
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
                "dbo.AbpUsers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        AuthenticationSource = c.String(maxLength: 64),
                        Name = c.String(nullable: false, maxLength: 32),
                        Surname = c.String(nullable: false, maxLength: 32),
                        UserName = c.String(nullable: false, maxLength: 32),
                        Password = c.String(nullable: false, maxLength: 128),
                        EmailAddress = c.String(nullable: false, maxLength: 256),
                        IsEmailConfirmed = c.Boolean(nullable: false),
                        EmailConfirmationCode = c.String(maxLength: 128),
                        PasswordResetCode = c.String(maxLength: 128),
                        LastLoginTime = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        Gender = c.Boolean(nullable: false),
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
                        IsActive = c.Boolean(nullable: false),
                        DomainName = c.String(maxLength: 50),
                        HQ = c.String(maxLength: 50),
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
                .ForeignKey("dbo.AbpUsers", t => t.LastModifierUserId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
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
                "dbo.AbpNotices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NoticeInfo = c.String(maxLength: 2000),
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
                "dbo.AbpOperatorsCompany",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(maxLength: 200),
                        Corporation = c.String(maxLength: 20),
                        TelePhone = c.String(maxLength: 20),
                        Address = c.String(maxLength: 100),
                        TenantId = c.Int(nullable: false),
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
                    { "DynamicFilter_OperatorsCompany_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_OperatorsCompany_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExtOtherAccount",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuthenticationSource = c.String(maxLength: 64),
                        Client = c.String(maxLength: 10),
                        UserName = c.String(maxLength: 20),
                        Name = c.String(),
                        Password = c.String(),
                        TelePhone = c.String(),
                        IsPhoneConfirmed = c.Boolean(nullable: false),
                        AccountLoginDatetime = c.DateTime(nullable: false),
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
                    { "DynamicFilter_OtherAccount_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExtOtherPlateNumber",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AssignedOtherAccountId = c.Int(),
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
                .ForeignKey("dbo.ExtOtherAccount", t => t.AssignedOtherAccountId)
                .Index(t => t.AssignedOtherAccountId);
            
            CreateTable(
                "dbo.AbpParks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegionId = c.Int(nullable: false),
                        ParkName = c.String(),
                        Mark = c.String(),
                        TenantId = c.Int(nullable: false),
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
                    { "DynamicFilter_Park_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Park_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpRegions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FatherId = c.Int(nullable: false),
                        RegionName = c.String(),
                        Mark = c.String(),
                        TenantId = c.Int(nullable: false),
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
                    { "DynamicFilter_Region_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Region_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.AbpSms",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SmsMsg = c.String(maxLength: 500),
                        SmsResult = c.String(maxLength: 500),
                        CreationTime = c.DateTime(nullable: false),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AbpTasks", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpRoles", "TenantId", "dbo.AbpTenants");
            DropForeignKey("dbo.AbpPermissions", "RoleId", "dbo.AbpRoles");
            DropForeignKey("dbo.AbpRoles", "LastModifierUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpRoles", "DeleterUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpRoles", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.ExtOtherPlateNumber", "AssignedOtherAccountId", "dbo.ExtOtherAccount");
            DropForeignKey("dbo.AbpNotices", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpMessages", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpMenus", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUsers", "TenantId", "dbo.AbpTenants");
            DropForeignKey("dbo.AbpSettings", "TenantId", "dbo.AbpTenants");
            DropForeignKey("dbo.AbpTenants", "LastModifierUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpTenants", "DeleterUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpTenants", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpSettings", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUserRoles", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpPermissions", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUserLogins", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUsers", "LastModifierUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUsers", "DeleterUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUsers", "CreatorUserId", "dbo.AbpUsers");
            DropIndex("dbo.AbpTasks", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpRoles", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpRoles", new[] { "LastModifierUserId" });
            DropIndex("dbo.AbpRoles", new[] { "DeleterUserId" });
            DropIndex("dbo.AbpRoles", new[] { "TenantId" });
            DropIndex("dbo.ExtOtherPlateNumber", new[] { "AssignedOtherAccountId" });
            DropIndex("dbo.AbpNotices", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpMessages", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpTenants", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpTenants", new[] { "LastModifierUserId" });
            DropIndex("dbo.AbpTenants", new[] { "DeleterUserId" });
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
            DropIndex("dbo.AbpMenus", new[] { "CreatorUserId" });
            DropTable("dbo.AbpTasks",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Task_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpSms");
            DropTable("dbo.AbpRoles",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Role_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Role_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpRegions",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Region_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Region_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpParks",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Park_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Park_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.ExtOtherPlateNumber",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_OtherPlateNumber_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.ExtOtherAccount",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_OtherAccount_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpOperatorsCompany",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_OperatorsCompany_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_OperatorsCompany_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpNotices",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Notice_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpMessages",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Message_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
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
                    { "DynamicFilter_User_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_User_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpMenus",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Menu_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Menu_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpIcons");
            DropTable("dbo.AbpDictionaryValue",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DictionaryValue_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpDictionaryType",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DictionaryType_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpDataPermissions",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DataPermission_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_DataPermission_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpCompanys",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Company_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpBerthsecs",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Berthsec_MustHavePark", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Berthsec_MustHaveRegion", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Berthsec_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Berthsec_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpBerths",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Berth_MustHaveBerthsec", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Berth_MustHavePark", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Berth_MustHaveRegion", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Berth_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpAuditLogs",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AuditLog_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_AuditLog_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
