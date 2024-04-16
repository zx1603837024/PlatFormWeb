namespace P4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VideoCar : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AbpVideoCars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantId = c.Int(),
                        CompanyId = c.Int(),
                        RegionId = c.Int(),
                        ParkId = c.Int(),
                        BerthsecId = c.Int(),
                        BerthNumber = c.String(maxLength: 20),
                        ParkStatus = c.Short(),
                        VideoCarNumber = c.String(maxLength: 30),
                        VedioEqType = c.Short(),
                        CreationTime = c.DateTime(nullable: false),
                        BeatDatetime = c.DateTime(),
                        Guid = c.Guid(),
                        IsOnlineValue = c.Short(),
                        IsUse = c.Short(),
                        BerthId = c.Int(),
                        VideoCarPlate = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpBerthsecs", t => t.BerthsecId)
                .Index(t => t.BerthsecId);
            
            CreateTable(
                "dbo.AbpVideoCarBusinessDetail",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        CompanyId = c.Int(),
                        RegionId = c.Int(),
                        ParkId = c.Int(),
                        BerthsecId = c.Int(),
                        BerthNumber = c.String(maxLength: 20),
                        VedioCarNumber = c.String(maxLength: 30),
                        Receivable = c.Decimal(precision: 18, scale: 2),
                        PlateNumber = c.String(maxLength: 10),
                        CarInTime = c.DateTime(),
                        CarOutTime = c.DateTime(),
                        StopTime = c.Int(),
                        guid = c.Guid(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        State = c.Int(nullable: false),
                        PlateColor = c.Int(nullable: false),
                        Confidence = c.Int(nullable: false),
                        Speed = c.Int(nullable: false),
                        VID = c.String(maxLength: 50),
                        BeforePathURL = c.String(maxLength: 250),
                        AfterPathURL = c.String(maxLength: 250),
                        OutBeforePathURL = c.String(maxLength: 250),
                        DetailOssPathURL = c.String(maxLength: 250),
                        OutAfterPathURL = c.String(maxLength: 250),
                        FixOssPathURL = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpVideoCarFault",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        CompanyId = c.Int(),
                        RegionId = c.Int(),
                        ParkId = c.Int(),
                        BerthsecId = c.Int(),
                        BerthNumber = c.String(maxLength: 20),
                        VedioCarNumber = c.String(maxLength: 30),
                        CreationTime = c.DateTime(nullable: false),
                        VID = c.String(maxLength: 50),
                        Status = c.String(),
                        Remark = c.String(),
                        StatusTime = c.String(),
                        OssPathURL = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AbpVideoCars", "BerthsecId", "dbo.AbpBerthsecs");
            DropIndex("dbo.AbpVideoCars", new[] { "BerthsecId" });
            DropTable("dbo.AbpVideoCarFault");
            DropTable("dbo.AbpVideoCarBusinessDetail");
            DropTable("dbo.AbpVideoCars");
        }
    }
}
