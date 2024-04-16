namespace P4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVideoEqBusDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpVideoEquipBusinessDetail", "State", c => c.Int(nullable: false));
            AddColumn("dbo.AbpVideoEquipBusinessDetail", "PlateColor", c => c.Int(nullable: false));
            AddColumn("dbo.AbpVideoEquipBusinessDetail", "OssPathURL", c => c.String(maxLength: 250));
            AddColumn("dbo.AbpVideoEquipBusinessDetail", "VModel", c => c.String());
            AddColumn("dbo.AbpVideoEquipBusinessDetail", "Powerp", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbpVideoEquipBusinessDetail", "Powerp");
            DropColumn("dbo.AbpVideoEquipBusinessDetail", "VModel");
            DropColumn("dbo.AbpVideoEquipBusinessDetail", "OssPathURL");
            DropColumn("dbo.AbpVideoEquipBusinessDetail", "PlateColor");
            DropColumn("dbo.AbpVideoEquipBusinessDetail", "State");
        }
    }
}
