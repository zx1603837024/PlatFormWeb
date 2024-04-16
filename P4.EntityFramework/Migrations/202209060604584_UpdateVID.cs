namespace P4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateVID : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AbpVideoEquipBusinessDetail", "VID", c => c.String());
            AlterColumn("dbo.AbpVideoEquipFaults", "VID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AbpVideoEquipFaults", "VID", c => c.Long(nullable: false));
            AlterColumn("dbo.AbpVideoEquipBusinessDetail", "VID", c => c.Long(nullable: false));
        }
    }
}
