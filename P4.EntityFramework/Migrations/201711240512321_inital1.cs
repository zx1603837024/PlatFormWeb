namespace P4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inital1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpCompanyFactoryExpress", "FactoryId", c => c.String(maxLength: 50));
            DropColumn("dbo.AbpCompanyFactoryExpress", "Customer");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AbpCompanyFactoryExpress", "Customer", c => c.String(maxLength: 50));
            DropColumn("dbo.AbpCompanyFactoryExpress", "FactoryId");
        }
    }
}
