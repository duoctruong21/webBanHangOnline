namespace webBangHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createTBstatistical : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Statisticals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        AccessCount = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.tb_product", "ViewCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_product", "ViewCount");
            DropTable("dbo.Statisticals");
        }
    }
}
