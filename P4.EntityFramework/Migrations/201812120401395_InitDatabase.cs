namespace P4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpPictureStores", "CreateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbpPictureStores", "CreateTime");
        }
    }
}
