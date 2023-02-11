namespace webBangHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateActive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_category", "isActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.tb_news", "isActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.tb_post", "isActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.tb_product", "isActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_product", "isActive");
            DropColumn("dbo.tb_post", "isActive");
            DropColumn("dbo.tb_news", "isActive");
            DropColumn("dbo.tb_category", "isActive");
        }
    }
}
