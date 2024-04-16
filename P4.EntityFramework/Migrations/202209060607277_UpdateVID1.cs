namespace P4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateVID1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AbpVideoEquipBusinessDetail", "VID", c => c.String(maxLength: 50));
            AlterColumn("dbo.AbpVideoEquipFaults", "VID", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AbpVideoEquipFaults", "VID", c => c.String());
            AlterColumn("dbo.AbpVideoEquipBusinessDetail", "VID", c => c.String());
        }
    }
}
