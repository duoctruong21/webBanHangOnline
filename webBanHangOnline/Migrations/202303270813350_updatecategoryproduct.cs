namespace webBangHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecategoryproduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_productCategory", "isDeleted", c => c.Boolean(nullable: false));
            DropColumn("dbo.tb_productCategory", "isDelete");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_productCategory", "isDelete", c => c.Boolean(nullable: false));
            DropColumn("dbo.tb_productCategory", "isDeleted");
        }
    }
}
