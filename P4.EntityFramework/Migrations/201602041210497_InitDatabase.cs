namespace P4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpTenants", "Sign", c => c.String(maxLength: 8));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbpTenants", "Sign");
        }
    }
}
