namespace P4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBerthId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpVideoEquips", "BerthId", c => c.Short());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbpVideoEquips", "BerthId");
        }
    }
}
