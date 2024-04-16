namespace P4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsSms : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpMonthlyCars", "IsSms", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbpMonthlyCars", "IsSms");
        }
    }
}
