namespace P4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Paylevel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AbpPayLevelSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreationTime = c.DateTime(nullable: false),
                        IsDelete = c.Short(),
                        DeviceType = c.Int(),
                        DeviceName = c.String(maxLength: 30),
                        DeviceOrder = c.Int(),
                        LastUser = c.String(maxLength: 30),
                        LastUpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AbpPayLevelSettings");
        }
    }
}
