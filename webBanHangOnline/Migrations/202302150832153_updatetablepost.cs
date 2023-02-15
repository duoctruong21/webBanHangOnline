namespace webBangHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetablepost : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_news", "Image", c => c.String(maxLength: 250));
            AlterColumn("dbo.tb_news", "SeoTitle", c => c.String(maxLength: 250));
            AlterColumn("dbo.tb_news", "SeoDescription", c => c.String(maxLength: 500));
            AlterColumn("dbo.tb_news", "SeoKeywords", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_news", "SeoKeywords", c => c.String());
            AlterColumn("dbo.tb_news", "SeoDescription", c => c.String());
            AlterColumn("dbo.tb_news", "SeoTitle", c => c.String());
            AlterColumn("dbo.tb_news", "Image", c => c.String());
        }
    }
}
