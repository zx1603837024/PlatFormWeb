namespace P4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateVideoEqPara : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AbpVideoEquips", "ParkStatus", c => c.Short());
            AlterColumn("dbo.AbpVideoEquips", "VedioEqType", c => c.Short());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AbpVideoEquips", "VedioEqType", c => c.Short(nullable: false));
            AlterColumn("dbo.AbpVideoEquips", "ParkStatus", c => c.Short(nullable: false));
        }
    }
}
