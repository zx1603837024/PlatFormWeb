namespace P4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBerthId1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AbpVideoEquips", "BerthId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AbpVideoEquips", "BerthId", c => c.Short());
        }
    }
}
