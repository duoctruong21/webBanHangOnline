namespace webBangHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_productCategory", "isActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_productCategory", "isActive");
        }
    }
}
