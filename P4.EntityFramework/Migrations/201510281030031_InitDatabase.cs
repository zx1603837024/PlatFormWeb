namespace P4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AbpBerths", "BerthsecId", "dbo.AbpBerthsecs");
            DropIndex("dbo.AbpBerths", new[] { "BerthsecId" });
            AddColumn("dbo.AbpBerths", "RelateNumber", c => c.String(maxLength: 10));
            AddColumn("dbo.AbpBerths", "InCarTime", c => c.DateTime());
            AddColumn("dbo.AbpBerths", "OutCarTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbpBerths", "OutCarTime");
            DropColumn("dbo.AbpBerths", "InCarTime");
            DropColumn("dbo.AbpBerths", "RelateNumber");
            CreateIndex("dbo.AbpBerths", "BerthsecId");
            AddForeignKey("dbo.AbpBerths", "BerthsecId", "dbo.AbpBerthsecs", "Id", cascadeDelete: true);
        }
    }
}
