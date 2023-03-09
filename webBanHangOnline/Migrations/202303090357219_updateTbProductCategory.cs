namespace webBangHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTbProductCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_productCategory", "isDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_productCategory", "isDeleted");
        }
    }
}
