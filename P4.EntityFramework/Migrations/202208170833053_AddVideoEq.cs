namespace P4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVideoEq : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpVideoEquips", "IsOnlineValue", c => c.Short(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbpVideoEquips", "IsOnlineValue");
        }
    }
}
