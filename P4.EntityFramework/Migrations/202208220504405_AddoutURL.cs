namespace P4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddoutURL : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpVideoEquipBusinessDetail", "OutOssPathURL", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbpVideoEquipBusinessDetail", "OutOssPathURL");
        }
    }
}
