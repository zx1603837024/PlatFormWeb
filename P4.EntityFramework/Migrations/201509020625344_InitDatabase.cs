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
            
            AlterTableAnnotations(
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
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Berth_MustHaveBerthsec",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AddColumn("dbo.AbpBerths", "BerthsecId", c => c.Int(nullable: false));
            AddColumn("dbo.AbpBerthsecs", "CheckInTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.AbpBerthsecs", "CheckInName", c => c.String(maxLength: 20));
            AddColumn("dbo.AbpBerthsecs", "CheckOutTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.AbpBerthsecs", "CheckOutName", c => c.String(maxLength: 20));
            AddColumn("dbo.AbpMessages", "ReviceUserId", c => c.Long(nullable: false));
            AddColumn("dbo.AbpMessages", "ReadTime", c => c.DateTime());
            AddColumn("dbo.AbpMessages", "IsRead", c => c.Boolean(nullable: false));
            AddColumn("dbo.AbpNotices", "ReviceUserId", c => c.Long(nullable: false));
            AddColumn("dbo.AbpNotices", "IsRead", c => c.Boolean(nullable: false));
            AddColumn("dbo.AbpNotices", "ReadTime", c => c.DateTime());
            AddColumn("dbo.AbpTasks", "ReadUserId", c => c.Long(nullable: false));
            AddColumn("dbo.AbpTasks", "ReadTime", c => c.DateTime());
            AddColumn("dbo.AbpTasks", "IsRead", c => c.Boolean(nullable: false));
            AlterColumn("dbo.AbpMenus", "Name", c => c.String(maxLength: 32));
            AlterColumn("dbo.AbpMenus", "FatherCode", c => c.String(maxLength: 30));
            AlterColumn("dbo.AbpMenus", "Level", c => c.String(maxLength: 5));
            AlterColumn("dbo.AbpMenus", "LocalizableString", c => c.String(maxLength: 64));
            AlterColumn("dbo.AbpMenus", "Icon", c => c.String(maxLength: 200));
            AlterColumn("dbo.AbpMenus", "Url", c => c.String(maxLength: 200));
            AlterColumn("dbo.AbpMenus", "RequiredPermissionName", c => c.String(maxLength: 30));
            AlterColumn("dbo.AbpMenus", "Discriminator", c => c.String(maxLength: 500));
            AlterColumn("dbo.AbpMessages", "Msg", c => c.String(maxLength: 500));
            AlterColumn("dbo.AbpNotices", "NoticeInfo", c => c.String(maxLength: 2000));
            AlterColumn("dbo.AbpTasks", "TaskInfo", c => c.String(maxLength: 500));
            DropColumn("dbo.AbpMenus", "Code");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AbpMenus", "Code", c => c.String());
            AlterColumn("dbo.AbpTasks", "TaskInfo", c => c.String());
            AlterColumn("dbo.AbpNotices", "NoticeInfo", c => c.String());
            AlterColumn("dbo.AbpMessages", "Msg", c => c.String());
            AlterColumn("dbo.AbpMenus", "Discriminator", c => c.String());
            AlterColumn("dbo.AbpMenus", "RequiredPermissionName", c => c.String());
            AlterColumn("dbo.AbpMenus", "Url", c => c.String());
            AlterColumn("dbo.AbpMenus", "Icon", c => c.String());
            AlterColumn("dbo.AbpMenus", "LocalizableString", c => c.String());
            AlterColumn("dbo.AbpMenus", "Level", c => c.String());
            AlterColumn("dbo.AbpMenus", "FatherCode", c => c.String());
            AlterColumn("dbo.AbpMenus", "Name", c => c.String());
            DropColumn("dbo.AbpTasks", "IsRead");
            DropColumn("dbo.AbpTasks", "ReadTime");
            DropColumn("dbo.AbpTasks", "ReadUserId");
            DropColumn("dbo.AbpNotices", "ReadTime");
            DropColumn("dbo.AbpNotices", "IsRead");
            DropColumn("dbo.AbpNotices", "ReviceUserId");
            DropColumn("dbo.AbpMessages", "IsRead");
            DropColumn("dbo.AbpMessages", "ReadTime");
            DropColumn("dbo.AbpMessages", "ReviceUserId");
            DropColumn("dbo.AbpBerthsecs", "CheckOutName");
            DropColumn("dbo.AbpBerthsecs", "CheckOutTime");
            DropColumn("dbo.AbpBerthsecs", "CheckInName");
            DropColumn("dbo.AbpBerthsecs", "CheckInTime");
            DropColumn("dbo.AbpBerths", "BerthsecId");
            AlterTableAnnotations(
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
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Berth_MustHaveBerthsec",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            DropTable("dbo.AbpOperatorsCompany",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_OperatorsCompany_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_OperatorsCompany_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
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
        }
    }
}
