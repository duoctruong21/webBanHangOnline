namespace webBangHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateProductCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_productCategory", "Alias", c => c.String(maxLength: 150));
            AlterColumn("dbo.tb_productCategory", "Icon", c => c.String(maxLength: 250));
            AlterColumn("dbo.tb_productCategory", "SeoTitle", c => c.String(maxLength: 250));
            AlterColumn("dbo.tb_productCategory", "SeoDescription", c => c.String(maxLength: 500));
            AlterColumn("dbo.tb_productCategory", "SeoKeywords", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_productCategory", "SeoKeywords", c => c.String());
            AlterColumn("dbo.tb_productCategory", "SeoDescription", c => c.String());
            AlterColumn("dbo.tb_productCategory", "SeoTitle", c => c.String());
            AlterColumn("dbo.tb_productCategory", "Icon", c => c.String());
            DropColumn("dbo.tb_productCategory", "Alias");
        }
    }
}
