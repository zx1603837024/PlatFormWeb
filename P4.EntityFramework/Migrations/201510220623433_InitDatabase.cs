namespace P4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDatabase : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AbpBerthsecs", "CustomNumeber", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AbpBerthsecs", "CustomNumeber", c => c.Int(nullable: false));
        }
    }
}
