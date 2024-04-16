namespace P4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDetFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpVideoEquipBusinessDetail", "DetailOssPathURL", c => c.String(maxLength: 250));
            AddColumn("dbo.AbpVideoEquipBusinessDetail", "FixOssPathURL", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbpVideoEquipBusinessDetail", "FixOssPathURL");
            DropColumn("dbo.AbpVideoEquipBusinessDetail", "DetailOssPathURL");
        }
    }
}
