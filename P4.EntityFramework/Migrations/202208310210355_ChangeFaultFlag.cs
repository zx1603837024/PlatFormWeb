namespace P4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFaultFlag : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AbpBerths", "IsFaultFlag", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AbpBerths", "IsFaultFlag", c => c.Boolean());
        }
    }
}
