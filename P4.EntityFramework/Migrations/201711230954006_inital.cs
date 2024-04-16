namespace P4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inital : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AbpReworkPicture",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReworkPictureId = c.Int(nullable: false),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AbpReworkPicture");
        }
    }
}
