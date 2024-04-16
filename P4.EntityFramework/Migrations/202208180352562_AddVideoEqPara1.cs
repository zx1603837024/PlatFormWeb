namespace P4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVideoEqPara1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpVideoEquips", "IsUse", c => c.Short());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbpVideoEquips", "IsUse");
        }
    }
}
