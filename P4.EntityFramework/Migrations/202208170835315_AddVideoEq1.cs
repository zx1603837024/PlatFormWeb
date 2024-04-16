namespace P4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVideoEq1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AbpVideoEquips", "IsOnlineValue", c => c.Short());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AbpVideoEquips", "IsOnlineValue", c => c.Short(nullable: false));
        }
    }
}
