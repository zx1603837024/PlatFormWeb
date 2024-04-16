namespace P4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVideoEqBusDetail1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpVideoEquipBusinessDetail", "VID", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbpVideoEquipBusinessDetail", "VID");
        }
    }
}
