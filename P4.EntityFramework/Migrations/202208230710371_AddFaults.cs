namespace P4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFaults : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AbpVideoEquipFaults",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        CompanyId = c.Int(),
                        RegionId = c.Int(),
                        ParkId = c.Int(),
                        BerthsecId = c.Int(),
                        BerthNumber = c.String(maxLength: 20),
                        VedioEqNumber = c.String(maxLength: 30),
                        CreationTime = c.DateTime(nullable: false),
                        VID = c.Long(nullable: false),
                        Status = c.String(),
                        Remark = c.String(),
                        StatusTime = c.String(),
                        OssPathURL = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AbpVideoEquipFaults");
        }
    }
}
